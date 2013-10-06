using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IMutationOperator<T>
        where T : IChromosome
    {
        void Mutate(T chromosome);
    }
}
