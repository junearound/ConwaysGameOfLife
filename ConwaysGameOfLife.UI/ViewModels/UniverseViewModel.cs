using ConwaysGameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.UI
{

    public class UniverseViewModel : ObservableBase
    {
        private readonly IUniverse _universe;
        private  int _simulationSpeed = 300;
        private Generation _initGeneration;

        public UniverseViewModel(int rows, int columns, int simulationSpeed)
        {
            //TODO IoC
            GenerationFactory factory = new GenerationFactory((int)CellState.Dead, rows, columns);
            IEvolutionStrategy strategy = new EvolutionStrategy(new ConwayCellRule(), new ConwayNeighborhoodRule(UniverseShape.Torus), factory);
            IGeneration initGeneration= factory.CreateGeneration();
            _universe = new Universe(strategy, initGeneration);

            this._simulationSpeed = simulationSpeed;
            this._rows = rows;
            this._columns = columns;
            EvolveCommand = new RelayCommand<object>(
                _ => EvolveGeneration(),
                _ => CanEvolveGeneration()
            );

            ResetCommand = new RelayCommand<object>(
                _ => ResetGame(),
                _ => CanResetGame()
            );

            ToggleCellCommand = new RelayCommand<object>(
                (cell) => ToggleCell(cell),
                _ => CanToggleCell()
            );


            PreviousGenerationCommand = new RelayCommand<object>(
                _ => PreviousGeneration(),
                _ => CanPreviousGeneration()
            );

            StartCommand = new RelayCommand<object>(
                _ => Start(),
                _ => CanStart()
            );


            StopCommand = new RelayCommand<object>(
                 _ => Stop(),
                 _ => CanStop()
            );
 
            UpdateGeneration();
            IsInitial = true;

        }

        #region Properties
        private int _rows;
        private int _columns;
        public int Rows { get { return _rows; } }
        public int Columns { get { return _columns; } }

        private int populationCount;
        public int PopulationCount
        {
            get { return populationCount; }
            private set
            {
                populationCount = value;
                OnPropertyChanged();
            }
        }

        private IGeneration _currentGeneration;
        public IGeneration CurrentGeneration
        {
            get { return _currentGeneration; }
            private set
            {
                _currentGeneration = value;
                OnPropertyChanged();
            }
        }

        private int generationNumber;
        public int GenerationNumber
        {
            get { return generationNumber; }
            private set
            {
                generationNumber = value;
                OnPropertyChanged();
            }
        }


        private bool _evolutionEnded;
        public bool EvolutionEnded
        {
            get { return _evolutionEnded; }
            private set
            {
                _evolutionEnded = value;
                OnPropertyChanged();
            }
        }

        private bool _isInitial;
        public bool IsInitial
        {
            get { return _isInitial; }
            private set
            {
                _isInitial = value;
                OnPropertyChanged();
            }
        }

        private bool _isRunning = false;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        private BackgroundWorker _worker;
        private BackgroundWorker Worker
        {
            get
            {
                if (_worker == null)
                    _worker = _createWorker();
                return _worker;
            }
        }
        private BackgroundWorker _createWorker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = false;

            worker.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                //int _simulationSpeed = (int)e.Argument;
             
                while (!worker.CancellationPending||!EvolutionEnded)
                {
                    
                    EvolveGeneration();
                    System.Threading.Thread.Sleep(300);
                }
              
            };
            worker.DoWork += delegate
            {
                IsRunning = false;
            };
            worker.RunWorkerCompleted += delegate
            {
                IsRunning = false;
            };
            

            return worker;
        }

        #endregion
        #region Command Properties
      
        public RelayCommand<object> EvolveCommand { get; private set; }
        public RelayCommand<object> ResetCommand { get; private set; }
        public RelayCommand<object> ToggleCellCommand { get; private set; }
        public RelayCommand<object> PreviousGenerationCommand { get; private set; }
        public RelayCommand<object> StartCommand { get; private set; }
        public RelayCommand<object> StopCommand { get; private set; }

        #endregion

        private void UpdateGeneration()
        {
            CurrentGeneration = _universe.CurrentGeneration;
            GenerationNumber = _universe.CurrentGenerationNumber;
            EvolutionEnded = _universe.IsEvolutionEnded;
            PopulationCount = _universe.CurrentGeneration.GetCount((int)CellState.Alive);
          
        }

        private void EvolveGeneration()
        {
            IGeneration generation = _universe.MoveNextGeneration();
            IsInitial = false;
            UpdateGeneration();
            if (EvolutionEnded)
                Stop();
        }

        private bool CanEvolveGeneration()
        {
            return !EvolutionEnded|| IsInitial;
        }

        private void ResetGame()
        {
            _universe.Reset();
            IsInitial = true;
            UpdateGeneration();
        }

        private bool CanResetGame()
        {
            return !IsRunning;
        }

        private void ToggleCell(object cellObj)
        {
            ICell cell = (ICell)cellObj;
            int newState = 0;
            if (cell.State == 0)
                newState = 1;
            _universe.CurrentGeneration[cell.Row, cell.Column] = new Cell(newState, cell.Row, cell.Column);
            UpdateGeneration();
        }
        private bool CanToggleCell()
        {
            return !IsRunning&&IsInitial&&GenerationNumber == 0 && _universe.GenerationsCount == 1;
        }

        private void PreviousGeneration()
        {
            IGeneration generation = _universe.MovePreviousGeneration();
            UpdateGeneration();
        }

        private bool CanPreviousGeneration()
        {
            return GenerationNumber > 0&& !IsRunning;
        }

        private void Start()
        {
            if (!Worker.IsBusy)
                Worker.RunWorkerAsync(_simulationSpeed); 
          

            IsRunning = true;
            IsInitial = false;
        }
        private bool CanStart()
        {
            return !IsRunning&& !EvolutionEnded&&PopulationCount>0;
        }

        private void Stop()
        {
            if (Worker.IsBusy)
                Worker.CancelAsync();
            UpdateGeneration();
            IsRunning = false;
        }
        private bool CanStop()
        {
            return IsRunning;
        }
    }
}
