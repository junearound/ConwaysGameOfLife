using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife.Logic
{
    public interface IGenerationFactory
    {
        IGeneration CreateEmpty();
        IGeneration CreateGeneration();
        int EmptyCellState { get; set; }
        int Rows { get; }
        int Columns { get; }
    }
}
