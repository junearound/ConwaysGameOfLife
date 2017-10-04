using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic 
{

    public class ConwayCellRule: ICellRule
    {
        public  ICell CreateNewCell(ICell currentCell, ICell[] neighborsState)
        {
            if (neighborsState != null)
            {
                int aliveCount = neighborsState.Count(s => s.State == 1);
                switch (currentCell.State)
                {
                    case 0:
                        if (aliveCount == 3)
                            return new Cell(1, currentCell.Row, currentCell.Column);
                        break;
                    case 1:
                        if (aliveCount > 3 || aliveCount < 2)
                            return new Cell(0, currentCell.Row, currentCell.Column);
                        break;

                }
            }
            return new Cell(currentCell.State, currentCell.Row, currentCell.Column);
        }
    }
}
