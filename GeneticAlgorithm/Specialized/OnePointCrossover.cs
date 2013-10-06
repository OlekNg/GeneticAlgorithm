using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics.Generic;

namespace Genetics.Specialized
{
    public class OnePointCrossover : ICrossoverOperator<BinaryChromosome>
    {
        /// <summary>
        /// To draw a slice point.
        /// </summary>
        private Random _randomizer;

        public OnePointCrossover()
        {
            _randomizer = new Random();
        }

        /// <summary>
        /// Crosses genotypes from given chromosomes and creates
        /// recombined genotypes.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>2 genotypes (2 element array of genotype).</returns>
        private List<bool>[] CreateNewGenotypes(List<bool> g1, List<bool> g2)
        {
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

            return newGenotypes;
        }

        public Tuple<IChromosome, IChromosome> Crossover(BinaryChromosome c1, BinaryChromosome c2)
        {
            List<bool>[] newGenotypes = CreateNewGenotypes(c1.Genotype, c2.Genotype);

            // Create new chromosomes.
            Tuple<IChromosome, IChromosome> result;
            result = new Tuple<IChromosome, IChromosome>(
                new BinaryChromosome(c1.Genotype),
                new BinaryChromosome(c2.Genotype));
            return result;
        }
    }
}
