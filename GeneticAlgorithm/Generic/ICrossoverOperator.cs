using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface ICrossoverOperator<T>
        where T : IChromosome
    {
        Tuple<IChromosome, IChromosome> Crossover(T chromosome1, T chromosome2);
    }
}
