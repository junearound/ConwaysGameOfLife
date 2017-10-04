using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{
    public interface IUniverse
    {
        bool IsEvolutionEnded { get; }
        void Reset();
        IGeneration MovePreviousGeneration();
        IGeneration MoveNextGeneration();
        IGeneration CurrentGeneration { get;   }
        IEvolutionStrategy EvolutionStrategy { get; set; }
        int CurrentGenerationNumber { get; }
        int GenerationsCount { get; }
 
    }
}
