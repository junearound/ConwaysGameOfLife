using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Logic
{
    public interface IGenerationFactory
    {
        IGeneration CreateEmpty();//int rows, int columns
        IGeneration CreateGeneration();//int rows, int columns
        //IGeneration CreateGeneration(ICell[,] cells);
        int EmptyCellState { get; set; }
        int Rows { get; }
        int Columns { get; }
    }
}
