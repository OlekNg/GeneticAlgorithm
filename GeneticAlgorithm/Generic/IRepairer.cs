using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IRepairer<T>
    {
        /// <summary>
        /// Should replace genotype with repaired one.
        /// </summary>
        void Repair(T genotype);
    }
}
