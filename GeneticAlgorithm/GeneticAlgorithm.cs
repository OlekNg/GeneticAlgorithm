using Genetics.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class GeneticAlgorithm
    {
        private const int DEFAULT_MAX_ITERATIONS = 1000;

        private List<IChromosome> _currentPopulation;

        public GeneticAlgorithm(ChromosomeFactory factory, int initialPopulationSize)
        {
            MaxIterations = DEFAULT_MAX_ITERATIONS;
            _currentPopulation = new List<IChromosome>(initialPopulationSize);

            for (int i = 0; i < initialPopulationSize; i++)
                _currentPopulation.Add(factory.Create());
        }

        /// <summary>
        /// Max iterations for genetic algorithm. Default 1000.
        /// </summary>
        public int MaxIterations { get; set; }

        /// <summary>
        /// Selector which creates new parent population.
        /// </summary>
        public ISelector Selector { get; set; }

        public void Start()
        {

        }
    }
}
