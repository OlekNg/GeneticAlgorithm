using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of genotype.</typeparam>
    public class Chromosome<T> : IChromosome
    {
        public Chromosome() { }
        public Chromosome(T genotype)
        {
            Genotype = genotype;
        }

        public static Func<T, IChromosome> CreateFunc { get; set; }
        public static ICrossoverOperator<Chromosome<T>> CrossoverOperator { get; set; }
        public static IEvaluator<Chromosome<T>> Evaluator { get; set; }
        public static IMutationOperator<Chromosome<T>> MutationOperator { get; set; }
        public static IRepairer<Chromosome<T>> Repairer { get; set; }

        public T Genotype { get; set; }
        public double Value { get; set; }

        public IChromosome Clone()
        {
            return CreateFunc(Genotype);
        }

        public Tuple<IChromosome, IChromosome> Crossover(IChromosome chromosome)
        {
            if (CrossoverOperator == null)
                return new Tuple<IChromosome, IChromosome>(this, chromosome);
            else
                return CrossoverOperator.Crossover(this, (Chromosome<T>)chromosome);
        }

        public void Eval()
        {
            if (Evaluator == null)
                throw new GeneticAlgorithmException("Evaluator not set. Evaluation cannot be performed.");

            Value = Evaluator.Eval(this);
        }

        public void Mutate()
        {
            if (MutationOperator != null)
                MutationOperator.Mutate(this);
        }

        public void Repair()
        {
            if (Repairer != null)
                Repairer.Repair(this);
        }
    }
}
