using Genetics.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public class GeneticAlgorithm
    {
        private const int DEFAULT_MAX_ITERATIONS = 1000;

        public GeneticAlgorithm()
        {
            MaxIterations = DEFAULT_MAX_ITERATIONS;
        }

        public void Start()
        {

        }

        public int MaxIterations { get; set; }
        public ISelector Selector { get; set; }
    }
}
