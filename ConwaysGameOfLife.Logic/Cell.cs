using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public struct Cell: ICell 
    {
        public Cell(int state, int row, int column) {
            _row = row;
            _column = column;
            _state = state;
        }
        public bool Equals(ICell other)
        {
            if (other == null)
                return false;
            if (_state == other.State&&this.Row==other.Row&& this.Column == other.Column)
                return true;
            return false;
        }
        #region Properties
        private int _state;
        public int State { get => _state; set => _state = value; }
        private int _row;
        public int Row { get => _row; set => _row = value; }
        private int _column;
        public int Column { get => _column; set => _column = value; }
        #endregion

    }
}
