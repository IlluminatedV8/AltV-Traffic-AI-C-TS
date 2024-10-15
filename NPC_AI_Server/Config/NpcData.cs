using System.Collections.Generic;


namespace homevserver.NpcVehicle.Config;


internal static class NPCData
{
    public static readonly List<string> SmallVehicle = new()
    {
        "asterope",
        "regina",
        "primo2",
        "washington",
        "emperor",
        "oracle",
        "dilettante",
        "stockade",
        "blade",
        "moonbeam",
        "virgo",
        "tampa",
        "voodoo",
        "rancherxl",
        "baller",
        "granger",
        "utillitruck2",
        "utillitruck3",
        "speedo",
        "surfer",
        "burrito",
        "bison3"
    };

    /// <summary>
    ///     Colors that will be randomly chosen from
    /// </summary>
    public static readonly List<byte> SmallVehicleColors = new()
    {
        15,
        16,
        17,
        18,
        19,
        20,
        43,
        44,
        45,
        56,
        57,
        75,
        76,
        77,
        78,
        79,
        80,
        81,
        108,
        109,
        110,
        122
    };


    public static readonly List<string> BigVehicle = new()
    {
        "trash2",
        "bus",
        "airbus",
        "benson",
        "pounder",
        "biff",
        "phantom",
        "packer",
        "mixer2",
        "rubble"
    };
}
