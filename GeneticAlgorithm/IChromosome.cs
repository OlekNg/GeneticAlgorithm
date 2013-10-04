using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    interface IChromosome
    {
        public IChromosome Crossover(IChromosome c);
        public IChromosome Mutate();
        public void Eval();

        /// <summary>s
        /// Vale of chromosome - effect of Eval function.
        /// </summary>
        public double Value { get; }
    }
}
