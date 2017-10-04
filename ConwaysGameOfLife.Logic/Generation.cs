using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public class Generation:IGeneration
    {
        public Generation(ICell[,] cells)
        {
            this._rows = cells.GetLength(0);
            this._columns = cells.GetLength(1);
            this._cells = cells;
        }
        public Generation(int rows, int columns)
        {
            this._rows = rows;
            this._columns = columns;
            this._cells = new ICell[rows,columns];
        }

        public ICell this[int row, int column]
        {
            get
            {
                if ((row < this._rows && row >= 0) && (column < this._columns && column >= 0))
                    return _cells[row, column];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if ((row < this._rows && row >= 0) && (column < this._columns && column >= 0))
                {
                    this._cells[row, column] = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public bool IsEmpty
        {
            get
            {
                  return Cells.Cast<ICell>().Count(c => c==null)>0;
            }
        }
        public int GetCount(int state)
        {
            if (Cells == null || this.Length == 0)
                return 0;
            return Cells.Cast<ICell>().Count(c => c != null && c.State == state);
        }
        public bool Equals(IGeneration other)
        {
            if (other == null)
                return false;
            if (other.Rows != this.Rows || other.Columns != this.Columns)
                return false;
            for (int i = 0; i < this._rows; i++)
            {
                for (int j = 0; j < this._columns; j++)//TODO has empty
                {
                    if (!other.Cells[i, j].Equals(this.Cells[i, j]))
                        return false;
                }
            }
            return true;
        }
        public IEnumerable<IEnumerable<ICell>> GetRows()
        {
            List<List<ICell>> rowsList = new List<List<ICell>>();
            for (int r = 0; r < this.Rows; r++)
            {
                List<ICell> list = new List<ICell>();
                rowsList.Add(new List<ICell>());
                for (int c = 0; c < this.Columns; c++)
                    list.Add(Cells[r, c]);

                rowsList.Add(list);
            }
            return rowsList;
        }

        #region Properties
        private ICell[,] _cells;
        public ICell[,] Cells { get => _cells; }
        private int _rows, _columns;
        public int Rows { get => _rows; }
        public int Columns { get => _columns; }
        public int Length { get => _columns*_rows; }
        #endregion

    }
}
