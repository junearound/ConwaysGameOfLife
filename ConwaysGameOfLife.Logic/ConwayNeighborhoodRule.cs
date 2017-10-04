using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public enum UniverseShape
    {
        //Endless,//0
        Bounded,//1
        Torus//3
    }



    public class ConwayNeighborhoodRule : INeighborhoodRule
    {
        public ConwayNeighborhoodRule(UniverseShape shape)
        {
            this._shape = shape;
        }
       

        public ICell[] GetNeighborhood(int row, int column, IGeneration generation)
        {
            ICell[] neighborhood = new ICell[8];


            if(this.IsBorderCell(row, column, generation.Rows, generation.Columns))
                return GetBorderNeighborhood(row, column, generation);
            int index = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (i == row && j == column)
                        continue;
                        neighborhood[index] = generation[i, j];
                    index++;

                }
            }
            return neighborhood;
        }
        private bool IsBorderCell(int row, int column, int rowsCount, int columnsCount)
        {
            return (row == 0 || column == 0 || row == (rowsCount - 1) || column == (columnsCount - 1));
        }
 
        public ICell[] GetBorderNeighborhood(int row, int column, IGeneration generation)
        {
            ICell[] neighborhood = new ICell[8];

            int index = 0;
            for (int i = row - 1; i <= row + 1; i++) 
            {
                for (int j = column - 1; j <= column + 1; j++)
                {

                    if (i == row && j == column)
                        continue;
                    if (this.UniverseShape == UniverseShape.Bounded)
                        neighborhood[index] = new Cell(0, row, column);
                    else
                    {
                        int r = this.GetNewIndex(i, generation.Rows), c = this.GetNewIndex(j, generation.Columns);
                        neighborhood[index] = generation[r, c];
                    }
                    index++;
                }
            }
            return neighborhood;
        }
        private int GetNewIndex(int index, int count) {
            if (index < 0)
                return  count - 1;
            if (index > count - 1)
                return 0;
            return index;
        }

        #region Properties
        private UniverseShape _shape = UniverseShape.Bounded;
        public UniverseShape UniverseShape { get => _shape; set => _shape = value; }
        #endregion
    }
}
