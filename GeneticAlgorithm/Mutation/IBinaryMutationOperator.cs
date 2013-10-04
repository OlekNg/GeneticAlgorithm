using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Mutation
{
    interface IBinaryMutationOperator
    {
        public BinaryChromosome Mutate(BinaryChromosome chromosome);
    }
}
