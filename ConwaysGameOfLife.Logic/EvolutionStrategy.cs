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
            //IList<Tuple<int, int, CellState>> cellLifeChangeTupleList = new List<Tuple<int, int, CellState>>();
            //Parallel.ForEach<IRule>(Rules, r => { evaluated |= r.Evaluate(cell, context); });


            IGeneration nextGeneration = this.GenerationFactory.CreateEmpty();// =   generation.Clone();// new Generation(generation.Rows, generation.Columns);//Copy?
            //generation.Cells.map
            for (int row = 0; row < generation.Rows; row++)
            {
                for (int column = 0; column < generation.Columns; column++)
                {
                    //lock (generation)


                    ICell cell = generation[row, column];
                    ICell[] neighbors = this.NeighborhoodRule.GetNeighborhood(row, column, generation);//Cell[,] allCells enum UniverseShape
                    ICell newCell = this.CellRule.CreateNewCell(cell, neighbors);
                    nextGeneration[row, column] = newCell;// new Cell(newState);//TODO row, column
                   // cellLifeChangeTupleList.Add(new Tuple<int, int, CellState>(row, column, newState));//TODO
                }
            }
            return nextGeneration;


            //create new Generation
            //if (cellLifeChangeTupleList.Any())
            //{
            //    CurrentGenerationNumber++;

            //    Parallel.ForEach(
            //        cellLifeChangeTupleList,
            //        tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
            //    );
            //}
 
        }
        public bool HasNextGeneration(IGeneration generation) {
            return !generation.IsEmpty&&generation.Length> generation.GetCount(GenerationFactory.EmptyCellState);
        }
        //private void CalculateCandidatePointsForNextGeneration()
        //{
        //    _aliveCellPoints.ForEach(aliveCellPoint =>
        //    {
        //        DiscoverNeighboursAndIncrementCount(aliveCellPoint);
        //        MarkAliveCellPointAsVisited(aliveCellPoint);
        //    });
        //}
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
