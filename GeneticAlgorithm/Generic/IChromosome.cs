using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IChromosome
    {
        double Value { get; set; }

        IChromosome Clone();
        Tuple<IChromosome, IChromosome> Crossover(IChromosome c);
        void Eval();
        void Mutate();
        void Repair();
    }
}
