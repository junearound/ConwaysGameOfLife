using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface INeighborhoodRule
    {
        ICell[] GetNeighborhood(int row,int column, IGeneration generation);
       
    }
}
