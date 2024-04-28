using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Business_System_Laboration_4.enums
{
    public enum Genre
    {
        [Description("Historia")]
        Historia,
        [Description("Kurslitteratur")]
        Kurslitteratur,
        [Description("Äventyr")]
        Äventyr,
        [Description("Noir")]
        Noir,
        [Description("Skräck")]
        Skräck
    }
}