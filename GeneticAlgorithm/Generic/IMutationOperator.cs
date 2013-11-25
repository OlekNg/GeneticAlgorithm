using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IMutationOperator<T>
    {
        /// <summary>
        /// Should replace genotype with mutated genotype.
        /// </summary>
        void Mutate(T genotype);
    }
}
