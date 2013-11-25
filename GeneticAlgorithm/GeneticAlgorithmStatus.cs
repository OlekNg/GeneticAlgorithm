using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Represents actual state of running genetic algorithm.
    /// </summary>
    public class GeneticAlgorithmStatus
    {
        /// <summary>
        /// Current iteration number.
        /// </summary>
        public int IterationNumber;
        public int MaxIterations;

        // Time consumption by each element.
        public double CrossoverOverhead;
        public double MutationOverhead;
        public double EvaluationOverhead;
        public double RepairOverhead;

        /// <summary>
        /// Current genetic algorithm population.
        /// </summary>
        public Population CurrentPopulation;
    }
}
