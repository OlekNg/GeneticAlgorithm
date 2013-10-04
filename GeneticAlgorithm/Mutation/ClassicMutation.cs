using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Mutation
{
    public class ClassicMutation : IBinaryMutationOperator
    {
        private const double DEFAULT_PROBABILITY = 0.1;

        /// <summary>
        /// To randomize mutation.
        /// </summary>
        private Random _randomizer;

        public ClassicMutation()
        {
            _randomizer = new Random();
        }

        public void Mutate(BinaryChromosome c)
        {
            Mutate(c, DEFAULT_PROBABILITY);
        }

        public void Mutate(BinaryChromosome c, double probability)
        {
            List<bool> genotype = c.Genotype;
            for (int i = 0; i < genotype.Count; i++)
            {
                if(_randomizer.NextDouble() <= probability)
                    genotype[i] = !genotype[i];
            }
        }
    }
}
