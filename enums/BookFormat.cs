using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Business_System_Laboration_4
{
    public enum BookFormat
    {
        [Description("Pocket")]
        Pocket,

        [Description("Inbunden")]
        Inbunden,

        [Description("E-bok")]
        E_Bok
    }
}