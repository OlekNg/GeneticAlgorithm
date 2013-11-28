using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface ICrossoverOperator<T>
    {
        Tuple<T, T> Crossover(Chromosome<T> c1, Chromosome<T> c2);
    }
}
