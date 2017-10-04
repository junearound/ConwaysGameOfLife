using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{

   public  class EvolutionStrategy: IEvolutionStrategy
    {
        public EvolutionStrategy(ICellRule cellRule, INeighborhoodRule neighborhoodRule, IGenerationFactory generationFactory)
        {
            this._cellRule = cellRule;
            this._neighborhoodRule = neighborhoodRule;
            this._generationFactory = generationFactory;
        }
 

    
        public IGeneration EvaluateGeneration(IGeneration generation) 
        {
            //TODO parallel by rows
            IGeneration nextGeneration = this.GenerationFactory.CreateEmpty();
            for (int row = 0; row < generation.Rows; row++)
            {
                for (int column = 0; column < generation.Columns; column++)
                {
                    ICell cell = generation[row, column];
                    ICell[] neighbors = this.NeighborhoodRule.GetNeighborhood(row, column, generation);
                    ICell newCell = this.CellRule.CreateNewCell(cell, neighbors);
                    nextGeneration[row, column] = newCell;
                }
            }
            return nextGeneration;
 
        }
        public bool HasNextGeneration(IGeneration generation) {
            return !generation.IsEmpty&&generation.Length> generation.GetCount(GenerationFactory.EmptyCellState);
        }
        #region Properties
        private ICellRule _cellRule;
        public ICellRule CellRule { get => _cellRule; set => _cellRule = value; }
        private INeighborhoodRule _neighborhoodRule;
        public INeighborhoodRule NeighborhoodRule { get => _neighborhoodRule; set => _neighborhoodRule = value; }
        private IGenerationFactory _generationFactory;
        public IGenerationFactory GenerationFactory { get => _generationFactory; set => _generationFactory = value; }
        #endregion
    }
}
