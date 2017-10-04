using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface IEvolutionStrategy
    {
        ICellRule CellRule { get; set; }
        INeighborhoodRule NeighborhoodRule { get; set; }
        IGenerationFactory GenerationFactory { get; set; }

        bool HasNextGeneration(IGeneration generation);
        IGeneration EvaluateGeneration(IGeneration generation);
    }
}
