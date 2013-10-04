using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public interface IChromosome
    {
        IChromosome Clone();
        Tuple<IChromosome, IChromosome> Crossover(IChromosome c);
        void Eval();
        void Mutate();
        void Repair();

        /// <summary>s
        /// Vale of chromosome - effect of Eval function.
        /// </summary>
        double Value { get; }
    }
}
