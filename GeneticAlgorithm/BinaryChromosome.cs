using Genetics.Crossover;
using Genetics.Evaluation;
using Genetics.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    class BinaryChromosome : IChromosome
    {
        private double _value;

        public BinaryChromosome()
        {
            Genotype = new List<bool>();
        }

        public BinaryChromosome(BinaryChromosome chromosome)
        {
            Genotype = new List<bool>(chromosome.Genotype);
        }

        public BinaryChromosome(IEnumerable<bool> genotype)
        {
            Genotype = new List<bool>(genotype);
        }

        public static IBinaryCrossoverOperator CrossoverOperator { get; set; }
        public static IBinaryMutationOperator MutationOperator { get; set; }
        public static IBinaryEvaluator Evaluator { get; set; }

        public List<bool> Genotype { get; set; }

        public double Value
        {
            get { return _value; }
        }

        public IChromosome Crossover(IChromosome c)
        {
            if (CrossoverOperator == null)
                throw new GeneticAlgorithmException("Crossover operator not set. Crossover cannot be performed.");

            return CrossoverOperator.Crossover(this, (BinaryChromosome)c);
        }

        public IChromosome Mutate()
        {
            if (MutationOperator == null)
                throw new GeneticAlgorithmException("Mutation operator not set. Mutation cannot be performed.");

            return MutationOperator.Mutate(this);
        }

        public void Eval()
        {
            if (Evaluator == null)
                throw new GeneticAlgorithmException("Evaluator not set. Evaluation cannot be performed.");

            _value = Evaluator.Eval(this);
        }
    }
}