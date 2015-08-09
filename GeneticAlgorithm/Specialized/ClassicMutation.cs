using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Specialized
{
    public class ClassicMutation : IMutationOperator<List<bool>>
    {
        private const double DEFAULT_PROBABILITY = 0.01;

        /// <summary>
        /// To randomize mutation.
        /// </summary>
        private Random _randomizer = new Random();
        private double _probability = DEFAULT_PROBABILITY;

        public ClassicMutation() { }

        public ClassicMutation(double probability)
        {
            _probability = probability;
        }

        public List<bool> Mutate(Chromosome<List<bool>> c)
        {
            List<bool> result = new List<bool>(c.Genotype);

            for (int i = 0; i < result.Count; i++)
            {
                if (_randomizer.NextDouble() <= _probability)
                    result[i] = !result[i];
            }

            return result;
        }

        public override string ToString()
        {
            return String.Format("Classic, {0}", _probability);
        }
    }
}
