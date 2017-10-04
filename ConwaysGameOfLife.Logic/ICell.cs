using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface ICell: IEquatable<ICell>
    {
        int State { get; set; }
        int Row { get; set; }
        int Column { get; set; }
        
    }
}
