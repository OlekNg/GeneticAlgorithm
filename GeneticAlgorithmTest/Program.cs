using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics.Generic;
using Genetics.Specialized;
using Genetics;

namespace GeneticAlgorithmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryChromosome.CrossoverOperator = new OnePointCrossover();
            BinaryChromosome.MutationOperator = new ClassicMutation();
            BinaryChromosome.Evaluator = new MyEvaluator();
            GeneticAlgorithm a = new GeneticAlgorithm(new BinaryChromosomeFactory(100), 200);
            a.Selector = new TournamentSelector();
            a.MaxIterations = 1000;
            a.ReportStatus += a_ReportStatus;

            a.Start();
            Console.WriteLine(a.BestChromosome.Value);
            Console.ReadKey();
        }

        static void a_ReportStatus(GeneticAlgorithmStatus status)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            Console.WriteLine("Iteration: {0}", status.IterationNumber);
            Console.WriteLine("Crossover overhead: {0:0.000}", status.CrossoverOverhead);
            Console.WriteLine("Evaluation overhead: {0:0.000}", status.EvaluationOverhead);
            Console.WriteLine("Mutation overhead: {0:0.000}", status.MutationOverhead);
            Console.WriteLine("Repair overhead: {0:0.000}", status.RepairOverhead);
            Console.WriteLine("Population fitness: {0:0.000}", status.CurrentPopulation.Fitness);
            Console.WriteLine("Population avg fitness: {0:0.000}", status.CurrentPopulation.AvgFitness);
            Console.WriteLine("Best population chromosome value: {0:0.000}", status.CurrentPopulation.BestChromosome.Value);
            Console.WriteLine("Best algorithm chromosome value: {0:0.000}", status.BestChromosome.Value);
        }
    }

    public class MyEvaluator : IEvaluator<List<bool>>
    {
        public double Eval(List<bool> genotype)
        {
            double result = 0;

            for (int i = 0; i < genotype.Count; i++)
            {
                if (genotype[i])
                    result += Math.Pow(i, 2);
            }

            return result;
        }
    }
}
