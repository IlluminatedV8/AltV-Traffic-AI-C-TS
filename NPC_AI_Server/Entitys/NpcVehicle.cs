using System;
using System.Numerics;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using homevserver.NpcVehicle.Config;


namespace homevserver.NpcVehicle.Entitys;


public class NPCVehicle
{
    public NPCVehicle( string popGroup, Vector3 position, Vector3 rotation, double rnd ){
        Player = null;
        byte Color = 0;
        string Model;

        if ( popGroup == "CITY_LARGE" || popGroup == "LARGE_CITY" )
        {
            Model = NPCData.BigVehicle[Convert.ToInt32(Math.Floor(rnd * NPCData.BigVehicle.Count))];
        }
        else
        {
            Model = NPCData.SmallVehicle[Convert.ToInt32(Math.Floor(rnd * NPCData.SmallVehicle.Count))];
            Color = NPCData.SmallVehicleColors[Convert.ToInt32(Math.Floor(rnd * NPCData.SmallVehicleColors.Count))];
        }

        Vehicle = Alt.CreateVehicle(Model, position, rotation);

        Vehicle.PrimaryColor = Color;
        Vehicle.SecondaryColor = Color;
        Vehicle.PearlColor = Color;

        Vehicle.Dimension = Global.Dimension;

        Vehicle.SetStreamSyncedMetaData("NPCVehicle", "civ");
    }

    public IPlayer Player { get; set; }
    public IVehicle Vehicle { get; set; }

    public async Task CheckVehicleNetworkOwner( ){
        if ( Vehicle.NetworkOwner == Player )
        {
            return;
        }

        var Transferred = Vehicle.NetworkOwner != null && Vehicle.NetworkOwner.Exists;

        if ( Transferred )
        {
            await Vehicle.NetworkOwner.EmitAsync("NPCVehicle:NetOwner", Vehicle, true, Transferred);
        }

        if ( Player != null && Player.Exists )
        {
            await Player.EmitAsync("NPCVehicle:NetOwner", Vehicle, false, Transferred);
        }

        Player = Vehicle.NetworkOwner;
    }
}
