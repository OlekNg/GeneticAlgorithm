using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Crossover
{
    interface IBinaryCrossoverOperator
    {
        public BinaryChromosome Crossover(BinaryChromosome c1, BinaryChromosome c2);
    }
}
