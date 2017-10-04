using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Logic
{

    public class Universe: IUniverse
    {
        
        private Dictionary<int, IGeneration> _generations=new Dictionary<int, IGeneration>();

        private IEvolutionStrategy _evolutionStrategy;
        public IEvolutionStrategy EvolutionStrategy { get => _evolutionStrategy; set => _evolutionStrategy = value; }
        public IGeneration  CurrentGeneration { get; private set; }
        public int CurrentGenerationNumber { get; private set; }

        public Universe(IEvolutionStrategy strategy, IGeneration initGeneration) {
            this._evolutionStrategy = strategy;
            this.CurrentGeneration = initGeneration;
            this.CurrentGenerationNumber = 0;
            this._generations.Add(0, initGeneration);
        }
         

        public IEnumerator<IGeneration> GetEnumerator()
        {
            while (!this.IsEvolutionEnded) {
                yield return MoveNextGeneration();
            }
        }
        public int GenerationsCount
        {
            get { return this._generations.Count; }
        }

        private bool HasNextGeneration(IGeneration  generation,  Dictionary<int, IGeneration > previousGenerations)
        {
            if (!this.EvolutionStrategy.HasNextGeneration(generation))
                return false;
            foreach (int key in previousGenerations.Keys)
            {
                if (generation.Equals(previousGenerations[key]))
                    return false;
            }
            return true;
        }

        public bool IsEvolutionEnded {
            get
            {
                return !this.HasNextGeneration(this.CurrentGeneration, 
                    this._generations.Where(k => k.Key < this.CurrentGenerationNumber).ToDictionary(x => x.Key, x => x.Value));
            }
        }
        public IGeneration MoveNextGeneration()
        {
            if (!this.IsEvolutionEnded)
            {
                this.CurrentGenerationNumber++;
                IGeneration generation;
                if (this._generations.ContainsKey(this.CurrentGenerationNumber))
                {
                    generation = this._generations[this.CurrentGenerationNumber];
                }
                else
                {
                    generation = this.EvolutionStrategy.EvaluateGeneration(this.CurrentGeneration);
                    this._generations.Add(this.CurrentGenerationNumber, generation);
                }
                this.CurrentGeneration = generation;
                return generation;
            }
            return null;
        }
        public IGeneration  MovePreviousGeneration()
        {
            if (this._generations.ContainsKey(this.CurrentGenerationNumber-1))
            {
                IGeneration generation = this._generations[this.CurrentGenerationNumber - 1];
                this.CurrentGeneration = generation;
                this.CurrentGenerationNumber--;
                return generation;
            }
            return null;
        }

        public void Reset() {
            _generations = new Dictionary<int, IGeneration>();
            this.CurrentGeneration = this.EvolutionStrategy.GenerationFactory.CreateGeneration();
            this.CurrentGenerationNumber = 0;
            this._generations.Add(0, this.CurrentGeneration);
        }
    

    }
}
