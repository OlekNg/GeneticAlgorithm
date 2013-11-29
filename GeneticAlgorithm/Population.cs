using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    /// <summary>
    /// Represents population of chromosomes.
    /// </summary>
    public class Population
    {
        protected double _avgFitness;
        protected double _fitness;
        protected IChromosome _bestChromosome;

        public Population()
        {
            Chromosomes = new List<IChromosome>();
        }

        public Population(List<IChromosome> chromosomes)
        {
            Chromosomes = chromosomes;
        }

        public Population(int size)
        {
            Chromosomes = new List<IChromosome>(size);
        }

        /// <summary>
        /// Population avarage fitness.
        /// </summary>
        public double AvgFitness
        {
            get
            {
                if (_avgFitness == default(double))
                    _avgFitness = CalculateAvgFitness();
                return _avgFitness;
            }
        }

        /// <summary>
        /// Best chromosome in current population.
        /// </summary>
        public IChromosome BestChromosome { get { return _bestChromosome; } }

        /// <summary>
        /// Population general fitness.
        /// </summary>
        public double Fitness
        {
            get
            {
                if (_fitness == default(double))
                    _fitness = CalculateFitness();
                return _fitness;
            }
        }

        /// <summary>
        /// Population count.
        /// </summary>
        public int Count { get { return Chromosomes.Count; } }

        /// <summary>
        /// Chromosomes in population.
        /// </summary>
        public List<IChromosome> Chromosomes { get; set; }

        /// <summary>
        /// Access to chromosome list in population through indexing operator.
        /// </summary>
        /// <param name="i">Index of chromosome.</param>
        /// <returns>Chromosome from population.</returns>
        public IChromosome this[int i]
        {
            get { return Chromosomes[i]; }
            set { Chromosomes[i] = value; }
        }

        /// <summary>
        /// Adds new chromosome to population.
        /// </summary>
        /// <param name="c"></param>
        public void Add(IChromosome c)
        {
            Chromosomes.Add(c);
        }

        /// <summary>
        /// Evaluates each chromosome in population and updates best one (can be accessed through BestChromosome property).
        /// </summary>
        public void Eval()
        {
            Chromosomes.ForEach(x => x.Eval());
            IChromosome candidate = Chromosomes.Where(x => x.Value == Chromosomes.Max(y => y.Value)).First();

            if (_bestChromosome == null)
            {
                _bestChromosome = candidate;
                return;
            }

            if (candidate.Value > _bestChromosome.Value)
                _bestChromosome = candidate;
        }

        /// <summary>
        /// Repaires each chromosome in population.
        /// </summary>
        public void Repair()
        {
            Chromosomes.ForEach(x => x.Repair());
        }

        /// <summary>
        /// Performs user defined transformations of chromosome -
        /// for example local search.
        /// </summary>
        public void Transform()
        {
            Chromosomes.ForEach(x => x.Transform());
        }

        /// <summary>
        /// Calculates population avarage fitness.
        /// </summary>
        protected double CalculateAvgFitness()
        {
            return Fitness / Count;
        }

        /// <summary>
        /// Calculates general population fitness.
        /// </summary>
        protected double CalculateFitness()
        {
            return Chromosomes.Sum(x => x.Value);
        }
    }
}
