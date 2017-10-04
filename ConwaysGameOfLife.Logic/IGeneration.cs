using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface IGeneration : IEquatable<IGeneration>//<T> : IEquatable<IGeneration<T>> where T : IEquatable<T>
    {
        ICell this[int row, int column]
        {
            get;
            set;
        }
        bool IsEmpty { get; }
        int GetCount(int state);
        IEnumerable<IEnumerable<ICell>> GetRows();


        int Length { get; }
        int Rows { get; }
        int Columns { get; }
        ICell[,] Cells { get; }
      

    }
}
