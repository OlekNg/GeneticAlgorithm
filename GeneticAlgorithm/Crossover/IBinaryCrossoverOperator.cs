using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Crossover
{
    public interface IBinaryCrossoverOperator
    {
        Tuple<IChromosome, IChromosome> Crossover(BinaryChromosome c1, BinaryChromosome c2);
    }
}
