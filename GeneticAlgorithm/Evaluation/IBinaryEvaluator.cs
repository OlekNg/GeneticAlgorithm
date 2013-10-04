using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Evaluation
{
    public interface IBinaryEvaluator
    {
        double Eval(BinaryChromosome chromosome);
    }
}
