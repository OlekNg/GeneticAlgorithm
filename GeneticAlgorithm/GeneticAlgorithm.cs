using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class GeneticAlgorithm<T>
        where T: IChromosome
    {
        private const int DEFAULT_MAX_ITERATIONS = 1000;

        private List<T> _currentPopulation;

        public GeneticAlgorithm(IChromosomeFactory<T> factory, int initialPopulationSize)
        {
            MaxIterations = DEFAULT_MAX_ITERATIONS;
            _currentPopulation = new List<T>(initialPopulationSize);

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
