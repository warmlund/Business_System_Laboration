using System.ComponentModel;

namespace Business_System_Laboration_4
{
    public enum BookFormat
    {
        [Description ("Ingen info")]
        None,

        [Description("Pocket")]
        Pocket,

        [Description("Inbunden")]
        Inbunden,

        [Description("E-bok")]
        E_Bok
    }
}