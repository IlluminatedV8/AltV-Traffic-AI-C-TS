using System.Collections.Generic;


namespace homevserver.NpcVehicle.Config;


internal class Global
{
    // How Many Vehicles to Spawn
    internal static readonly ushort VehicleAmount = 0;

    // Default Dimension for NPC Vehicle
    internal static readonly int Dimension = 1;

    // Only Spots with this Specific PopGroup will be used
    internal static readonly List<string> PopFilter = new() { "", "CITY_LARGE", "LARGE_CITY", "FREEWAY" };

    // Which prefix will be shown in Server Logs
    internal static readonly string LogPrefix = "[XineticVehicles]";

    // The internal JSON Path
    internal static readonly string JSONPath = "./";
}
