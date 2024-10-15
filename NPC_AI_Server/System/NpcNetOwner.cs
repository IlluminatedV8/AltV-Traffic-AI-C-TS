using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;


namespace homevserver.NpcVehicle.System;


public class NPCNetOwner : IScript
{
    public NPCNetOwner( ){
        IsRunning = true;
        Handler = new NPCVehicleHandler();
    }

    private static bool IsRunning { get; set; }

    private NPCVehicleHandler Handler { get; }

    [AsyncServerEvent("NPCVehicleSync::Start")]
    public async Task StartAsync( ){
        Alt.LogWarning($"{Global.LogPrefix}Starting To Sync NetOwner Data...");

        await CheckNetOwner();
    }

    public async Task CheckNetOwner( ){
        while ( Main.Running )
        {
            await Handler.CheckNetOwnerAsync();
            await Task.Delay(1000);
        }
    }

    [ServerEvent("NPCVehicleSync::Stop")]
    public void StopAsync( ){ IsRunning = false; }
}
