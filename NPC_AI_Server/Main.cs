using AltV.Net;
using homevserver.NpcVehicle.System;


namespace homevserver.NpcVehicle;


internal sealed class Main
{
    internal NPCVehicleHandler Handler { get; set; }

    public static bool Running { get; set; }

    public void OnStart( ){
        Alt.LogWarning($"Starting AI System...");
    }

    public void OnStop( ){
        Alt.Emit("NPCVehicleSync::Stop");
        Running = false;
    }
}
