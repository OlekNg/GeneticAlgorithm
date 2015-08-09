using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Main genetic algorithm class.
    /// </summary>
    public class GeneticAlgorithm
    {
        #region Fields
        protected const double DEFAULT_CROSSOVER_PROBABILITY = 0.75;
        protected const int DEFAULT_MAX_ITERATIONS = 100;
        protected const int DEFAULT_POP_SIZE = 50;

        // Measuring time of each genetic algorithm phase.
        Stopwatch _swCrossover = new Stopwatch();
        Stopwatch _swMutation = new Stopwatch();
        Stopwatch _swEvaluation = new Stopwatch();
        Stopwatch _swRepair = new Stopwatch();
        Stopwatch _swTransform = new Stopwatch();
        Stopwatch _swSelection = new Stopwatch();
        double _lastIterationTime = 0;

        /// <summary>
        /// Iteration counter.
        /// </summary>
        protected int _currentIteration;

        /// <summary>
        /// Chromosome factory which is used to create initial population.
        /// </summary>
        protected IChromosomeFactory _factory;

        /// <summary>
        /// Current chromosome population.
        /// </summary>
        protected Population _currentPopulation;

        /// <summary>
        /// Parent chromosome population.
        /// </summary>
        protected Population _parentPopulation;

        /// <summary>
        /// Number of chromosomes in initial population.
        /// </summary>
        protected int _initialPopulationSize = DEFAULT_POP_SIZE;

        /// <summary>
        /// Indicates if we should stop running algorithm.
        /// </summary>
        protected bool _stopAlgorithm = false;

        protected Random _randomizer = new Random();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes genetic algorithm with default initial population size.
        /// </summary>
        /// <param name="factory">Chromosome factory to perform population initialization.</param>
        public GeneticAlgorithm(IChromosomeFactory factory)
        {
            MaxIterations = DEFAULT_MAX_ITERATIONS;
            CrossoverProbability = DEFAULT_CROSSOVER_PROBABILITY;
            _factory = factory;

            // No default stop condition - always return false.
            CheckStopCondition = (p1, p2) => false;
        }

        /// <summary>
        /// Initializes genetic algorithm with desired initial population size.
        /// </summary>
        /// <param name="factory">Chromosome factory to perform population initialization.</param>
        /// <param name="initialPopulationSize">Initial population size.</param>
        public GeneticAlgorithm(IChromosomeFactory factory, int initialPopulationSize)
            : this(factory)
        {
            _initialPopulationSize = initialPopulationSize;
        }
        #endregion

        public delegate void ReportStatusDelegate(GeneticAlgorithmStatus status);

        /// <summary>
        /// Reports status of running genetic algorithm.
        /// </summary>
        public event ReportStatusDelegate ReportStatus;

        /// <summary>
        /// Reports status after algorithm is completed.
        /// </summary>
        public event ReportStatusDelegate Completed;

        #region Properties
        /// <summary>
        /// Best chromosome during whole algorithm.
        /// </summary>
        public IChromosome BestChromosome { get; set; }

        /// <summary>
        /// Function for checking for stop condition.
        /// </summary>
        public Func<Population, Population, bool> CheckStopCondition { get; set; }

        /// <summary>
        /// Probability of taking chromosome to crossover operation.
        /// </summary>
        public double CrossoverProbability { get; set; }

        /// <summary>
        /// Returns current iteration number.
        /// </summary>
        public int CurrentIteration { get { return _currentIteration; } }

        /// <summary>
        /// Access to current population.
        /// </summary>
        public Population CurrentPopulation { get { return _currentPopulation; } }

        /// <summary>
        /// Max iterations for genetic algorithm. Default 1000.
        /// </summary>
        public int MaxIterations { get; set; }

        /// <summary>
        /// Selector which creates new current population based on parent population.
        /// </summary>
        public ISelector Selector { get; set; }
        #endregion

        /// <summary>
        /// Stops algorithm if was running.
        /// </summary>
        public void Stop()
        {
            _stopAlgorithm = true;
        }

        /// <summary>
        /// Performs whole genetic algorithm cycle except checking for stop conditions.
        /// Allows to manually run genetic algorithm.
        /// </summary>
        public void NextGeneration()
        {
            _currentIteration++;
            _parentPopulation = _currentPopulation;

            // Perform whole genetic algorithm cycle.
            SelectionPhase();
            CrossoverPhase();
            MutationPhase();
            ReparationPhase();
            TransformPhase();
            EvaluationPhase();

            if (ReportStatus != null)
                ReportStatus(GenerateReportStatus());
        }

        

        #region Genetic algorithm phases.
        /// <summary>
        /// Performs population crossover.
        /// </summary>
        protected void CrossoverPhase()
        {
            _swCrossover.Start();

            // List of chromosomes to crossover.
            List<IChromosome> toCrossover = new List<IChromosome>();

            // Draw chromosomes to crossover.
            for (int i = 0; i < _currentPopulation.Count; i++)
            {
                if (_randomizer.NextDouble() < CrossoverProbability)
                    // Insert at random position.
                    toCrossover.Insert(_randomizer.Next(0, toCrossover.Count), _currentPopulation[i]);
            }

            // Check if we can pair chromosomes and optionally remove one.
            if (toCrossover.Count % 2 != 0)
                toCrossover.RemoveAt(_randomizer.Next(0, toCrossover.Count));

            // Chromosomes are in random order, so we can pair them sequentially.
            for (int i = 0; i < toCrossover.Count; i += 2)
                toCrossover[i].Crossover(toCrossover[i + 1]);

            _swCrossover.Stop();
        }

        /// <summary>
        /// Evaluates population.
        /// </summary>
        protected void EvaluationPhase()
        {
            _swEvaluation.Start();
            _currentPopulation.Eval();
            _swEvaluation.Stop();

            // Update best chromosome during whole algorithm.
            if (BestChromosome == null)
                BestChromosome = _currentPopulation.BestChromosome;
            else
                if (_currentPopulation.BestChromosome.CompareTo(BestChromosome) > 0)
                    BestChromosome = _currentPopulation.BestChromosome;
        }

        /// <summary>
        /// Performs population mutation.
        /// </summary>
        protected void MutationPhase()
        {
            _swMutation.Start();
            _currentPopulation.Chromosomes.ForEach(x => x.Mutate());
            _swMutation.Stop();
        }

        /// <summary>
        /// Repaires population.
        /// </summary>
        protected void ReparationPhase()
        {
            _swRepair.Start();
            _currentPopulation.Repair();
            _swRepair.Stop();
        }

        /// <summary>
        /// Creates new current population based on parent population using Selector.
        /// </summary>
        protected void SelectionPhase()
        {
            _swSelection.Start();
            _currentPopulation = Selector.Select(_parentPopulation);
            _swSelection.Stop();
        }

        protected void TransformPhase()
        {
            _swTransform.Start();
            _currentPopulation.Transform();
            _swTransform.Stop();
        }
        #endregion

        /// <summary>
        /// Starts algorithm.
        /// </summary>
        public void Start()
        {
            _stopAlgorithm = false;
            InitPopulation();
            ResetTimers();
            BestChromosome = null;

            do
            {
                NextGeneration();
            } while (!CheckStopCondition(_currentPopulation, _parentPopulation) && _currentIteration < MaxIterations && !_stopAlgorithm);

            if(Completed != null)
                Completed(GenerateReportStatus());
        }

        /// <summary>
        /// Generates current status of algorithm
        /// </summary>
        protected GeneticAlgorithmStatus GenerateReportStatus()
        {
            // Sum all execution times.
            double sum = _swCrossover.Elapsed.TotalMilliseconds;
            sum += _swEvaluation.Elapsed.TotalMilliseconds;
            sum += _swMutation.Elapsed.TotalMilliseconds;
            sum += _swRepair.Elapsed.TotalMilliseconds;
            sum += _swTransform.Elapsed.TotalMilliseconds;
            sum += _swSelection.Elapsed.TotalMilliseconds;

            GeneticAlgorithmStatus status = new GeneticAlgorithmStatus();
            status.CrossoverOverhead = _swCrossover.Elapsed.TotalMilliseconds / sum;
            status.EvaluationOverhead = _swEvaluation.Elapsed.TotalMilliseconds / sum;
            status.MutationOverhead = _swMutation.Elapsed.TotalMilliseconds / sum;
            status.RepairOverhead = _swRepair.Elapsed.TotalMilliseconds / sum;
            status.TransformOverhead = _swTransform.Elapsed.TotalMilliseconds / sum;
            status.SelectionOverhead = _swSelection.Elapsed.TotalMilliseconds / sum;

            status.IterationNumber = _currentIteration;
            status.MaxIterations = MaxIterations;
            status.IterationTimeInMillis = sum - _lastIterationTime;
            _lastIterationTime = sum;

            status.CurrentPopulation = _currentPopulation;
            status.BestChromosome = BestChromosome;

            return status;
        }

        /// <summary>
        /// Initializes genetic algorithm population.
        /// </summary>
        protected void InitPopulation()
        {
            _currentPopulation = new Population();
            for (int i = 0; i < _initialPopulationSize; i++)
                _currentPopulation.Chromosomes.Add(_factory.Create());

            _currentPopulation.Repair();
            _currentPopulation.Eval();
        }

        /// <summary>
        /// Resets all timers responsible for measuring each phase execution time.
        /// </summary>
        protected void ResetTimers()
        {
            _swCrossover.Reset();
            _swEvaluation.Reset();
            _swMutation.Reset();
            _swRepair.Reset();
        }
    }
}
