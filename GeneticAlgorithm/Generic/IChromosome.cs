using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IChromosome : IComparable<IChromosome>
    {
        double Value { get; set; }

        /// <summary>
        /// Should perform deep copy of chromosome.
        /// </summary>
        IChromosome Clone();

        /// <summary>
        /// Should replace genotypes of local chromosome and c chormosome with
        /// recombined ones.
        /// </summary>
        /// <param name="c">Second chromosome to perform crossover with.</param>
        void Crossover(IChromosome c);

        /// <summary>
        /// Should evaluate chromosome and result insert into Value property.
        /// </summary>
        void Eval();

        /// <summary>
        /// Should replace genotype with mutated genotype.
        /// </summary>
        void Mutate();

        /// <summary>
        /// Should replace genotype with repaired one.
        /// </summary>
        void Repair();
    }
}
