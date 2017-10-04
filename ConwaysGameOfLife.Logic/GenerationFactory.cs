using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Logic
{
    public class GenerationFactory:IGenerationFactory
    {
        public GenerationFactory(int defaultState, int rows, int columns)
        {
            this._emptyCellState = defaultState;
            this._rows = rows;
            this._columns = columns;
        }
    
        public IGeneration CreateEmpty()
        {
            return new Generation(this.Rows, this.Columns);
        }
        public IGeneration CreateGeneration()
        {
            Generation generation =  new Generation(this.Rows, this.Columns);
            for (int i = 0; i < generation.Rows; i++)
            {
                for (int j = 0; j < generation.Columns; j++)
                {
                    generation[i, j] = new Cell(this.EmptyCellState, i, j);
                }
            }
            return generation;
        }

        #region Properties
        private int _emptyCellState = 0;
        public int EmptyCellState { get => _emptyCellState; set => _emptyCellState = value; }
        private int _rows, _columns;
        public int Rows { get => _rows; }
        public int Columns { get => _columns; }
        #endregion
    }
}
