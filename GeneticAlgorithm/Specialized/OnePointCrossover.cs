using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics.Generic;

namespace Genetics.Specialized
{
    public class OnePointCrossover : ICrossoverOperator<List<bool>>
    {
        /// <summary>
        /// To draw a slice point.
        /// </summary>
        private Random _randomizer;

        public OnePointCrossover()
        {
            _randomizer = new Random();
        }

        public Tuple<List<bool>, List<bool>> Crossover(Chromosome<List<bool>> c1, Chromosome<List<bool>> c2)
        {
            var g1 = c1.Genotype;
            var g2 = c2.Genotype;

            int length = g1.Count;
            int point = _randomizer.Next(1, length);

            List<bool>[] newGenotypes = new List<bool>[2];
            newGenotypes[0] = new List<bool>(length);
            newGenotypes[1] = new List<bool>(length);

            // First genotype.
            newGenotypes[0].AddRange(g1.GetRange(0, point));
            newGenotypes[0].AddRange(g2.GetRange(point, length - point));

            // Second genotype.
            newGenotypes[1].AddRange(g2.GetRange(0, point));
            newGenotypes[1].AddRange(g1.GetRange(point, length - point));

            return new Tuple<List<bool>, List<bool>>(newGenotypes[0], newGenotypes[1]);
        }
    }
}
