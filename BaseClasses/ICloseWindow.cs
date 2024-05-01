using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_System_Laboration_4
{
    public interface ICloseWindow
    {
        Action Close { get; set; }
    }
}
