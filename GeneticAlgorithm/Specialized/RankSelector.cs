using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Specialized
{
    public class RankSelector : ISelector
    {
        private Random _randomizer = new Random();

        public Population Select(Population population)
        {
            int bestCount = (int)(0.25 * population.Count);

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
