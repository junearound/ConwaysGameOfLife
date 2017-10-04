using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface ICellRule
    {
        ICell CreateNewCell(ICell currentCell, ICell[] neighbors);
        //private readonly Predicate<ICell, ICell[]> _condition;
    }
}
