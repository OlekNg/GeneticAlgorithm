using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Crossover
{
    public class OnePointCrossover : IBinaryCrossoverOperator
    {
        /// <summary>
        /// To draw a slice point.
        /// </summary>
        private Random _randomizer;

        public OnePointCrossover()
        {
            _randomizer = new Random();
        }

        Tuple<IChromosome, IChromosome> IBinaryCrossoverOperator.Crossover(BinaryChromosome c1, BinaryChromosome c2)
        {
            CheckChromosomes(c1, c2);
            List<bool>[] newGenotypes = CreateNewGenotypes(c1, c2);

            // Create new chromosomes.
            Tuple<IChromosome, IChromosome> result;
            result = new Tuple<IChromosome, IChromosome>(new BinaryChromosome(newGenotypes[0]), new BinaryChromosome(newGenotypes[1]));
            return result;
        }

        /// <summary>
        /// Checks if crossing over is possible for given chromosomes.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        private void CheckChromosomes(BinaryChromosome c1, BinaryChromosome c2)
        {
            if (c1.Length != c2.Length)
                throw new GeneticAlgorithmException("Illegal operation! One point crossover " +
                                             "cannot be done with different length chromosomes.");

            if (c1.Length < 2)
                throw new GeneticAlgorithmException("Illegal operation! One point crossover " +
                                             "requires at least 2-long chromosome.");
        }

        /// <summary>
        /// Crosses genotypes from given chromosomes and creates
        /// recombined genotypes.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>2 genotypes (2 element array of genotype).</returns>
        private List<bool>[] CreateNewGenotypes(BinaryChromosome c1, BinaryChromosome c2)
        {
            int length = c1.Length;

            // Slicing point.
            // Drawing from 1 to length, because Next(min, max)
            // actually draws from min to (max-1).
            int point = _randomizer.Next(1, length);

            // Combine and create new genotypes.
            List<bool> g1 = c1.Genotype;
            List<bool> g2 = c2.Genotype;

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
    }
}
