using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Genetics.Specialized
{
    public class RankSelector : ISelector
    {
        protected const SelectionMode DEFAULT_MODE = SelectionMode.Percentage;
        protected const int DEFAULT_NUMBER = 10;
        protected const double DEFAULT_PERCENTAGE = 0.5;

        protected Random _randomizer = new Random();

        public RankSelector()
        {
            Mode = DEFAULT_MODE;
            Number = DEFAULT_NUMBER;
            Percentage = DEFAULT_PERCENTAGE;
        }

        public RankSelector(SelectionMode mode)
            : this()
        {
            Mode = mode;
        }

        /// <summary>
        /// Number - number best chromosomes will be taken to new population.
        /// Percentage - certain percentage of best chromosomes will be taken to new population.
        /// </summary>
        public enum SelectionMode { Number, Percentage }

        /// <summary>
        /// Selection mode.
        /// </summary>
        public SelectionMode Mode { get; set; }

        /// <summary>
        /// Number of best chromosomes from parent population in Number selection mode.
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// Percentage of best chromosomes from parent population in Percentage selection mode.
        /// </summary>
        public double Percentage { get; set; }

        public Population Select(Population population)
        {
            int bestCount = Mode == SelectionMode.Percentage ? (int)(Percentage * population.Count) : Number;

            // Create ranking
            List<IChromosome> ranking = population.Chromosomes.OrderByDescending(x => x.Value).ToList();

            List<IChromosome> newPopulation = new List<IChromosome>();
            newPopulation.AddRange(ranking.Take(bestCount).Select(x => x.Clone()));

            // Reproduce to rise population size to source population size.
            int reproduceCount = population.Count - bestCount;

            for (int i = 0; i < reproduceCount; i++)
                newPopulation.Add(newPopulation[_randomizer.Next(0, bestCount)].Clone());

            return new Population(newPopulation);
        }

    }
}
