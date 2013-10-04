using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Evaluation
{
    interface IBinaryEvaluator
    {
        public double Eval(BinaryChromosome chromosome);
    }
}
