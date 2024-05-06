using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public enum PlatformType
    {
        [Description("Ingen info")]
        None,
        [Description("PC")]
        PC,
        [Description("Playstation 5")]
        Playstation_5,
        [Description("Switch")]
        Switch,
        [Description("XBOX")]
        XBOX,
        [Description("iOS")]
        iOS
    }
}