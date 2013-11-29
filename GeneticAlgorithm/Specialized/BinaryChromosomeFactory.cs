using Genetics.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Specialized
{
    public class BinaryChromosomeFactory : IChromosomeFactory
    {
        protected int _chromosomeLength;

        protected Random _randomizer = new Random();

        public BinaryChromosomeFactory(int chromosomeLength)
        {
            _chromosomeLength = chromosomeLength;
        }

        public IChromosome Create()
        {
            List<bool> genotype = new List<bool>(_chromosomeLength);

            for (int i = 0; i < _chromosomeLength; i++)
                genotype.Add(_randomizer.Next(2) == 0 ? true : false);

            return new BinaryChromosome(genotype);
        }
    }
}
