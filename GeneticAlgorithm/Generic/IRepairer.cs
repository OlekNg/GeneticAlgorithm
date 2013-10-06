using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IRepairer<T>
        where T : IChromosome
    {
        void Repair(T chromosome);
    }
}
