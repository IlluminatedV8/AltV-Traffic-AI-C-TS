using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;

using Newtonsoft.Json;


namespace Server.NpcVehicle.System;


internal class NPCVehicleHandler : IScript
{
    public NPCVehicleHandler( ){ Vehicles = new List<NPCVehicle>(); }

    public static List<NPCVehicle> Vehicles { get; private set; }


    [AsyncServerEvent("NPCVehicleSync::Init")]
    public async Task Init( ){
        Vehicles = new List<NPCVehicle>();

        var rnd = new Random(DateTime.Now.Millisecond);

        var data = await File.ReadAllTextAsync($"{Global.JSONPath}SpawnPoints.json");

        var Data = JsonConvert.DeserializeObject<List<NPCVehicleData>>(data);

        Alt.LogWarning($"{Global.LogPrefix}Loaded ~lr~{Data.Count}~w~ NPCVehicleData");

        var Filtered = Data.FindAll(x => Global.PopFilter.Contains(x.PopGroup));

        Filtered = Data.OrderBy(x => rnd.NextDouble()).Take(Global.VehicleAmount).ToList();

        Alt.LogWarning($"{Global.LogPrefix}Selected ~lr~{Filtered.Count}~w~ NPCVehicleData");

        var SmallVehicle = 0;
        var BigVehicle = 0;

        await AltAsync.Do(( ) =>
            {
                for ( var i = 0;
                     i < Global.VehicleAmount;
                     i++ )
                {
                    var n = Filtered[i];
                    var Pos = n.Position;
                    var Rot = new Vector3(0, 0, 0 - ( float )Math.Atan2(Convert.ToDouble(n.OrientX), Convert.ToDouble(n.OrientY)));

                    if ( n.PopGroup == "CITY_LARGE" || n.PopGroup == "LARGE_CITY" )
                    {
                        BigVehicle++;
                    }
                    else
                    {
                        SmallVehicle++;
                    }

                    Vehicles.Add(new NPCVehicle(n.PopGroup, Pos, Rot, rnd.NextDouble()));
                }
            });

        Alt.LogWarning($"{Global.LogPrefix}Spawned ~lr~{SmallVehicle}~w~ SmallVehicle");
        Alt.LogWarning($"{Global.LogPrefix}Spawned ~lr~{BigVehicle}~w~ BigVehicle");

        Alt.Emit("NPCVehicleSync::Start");
    }


    public async Task CheckNetOwnerAsync( ){
        foreach ( var v in Vehicles )
        {
            if ( v.Vehicle.NetworkOwner == null )
            {
                continue;
            }

            try
            {
                await v.CheckVehicleNetworkOwner();
            }
            catch ( Exception e )
            {
                Alt.LogWarning($"Fehler beim Überprüfen des Fahrzeugs {v.Vehicle.Id} von {v.Vehicle.NetworkOwner.Name}: {e.Message}");
            }
        }
    }

}
