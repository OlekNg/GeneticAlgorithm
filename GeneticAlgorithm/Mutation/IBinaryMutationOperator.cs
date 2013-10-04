using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Mutation
{
    public interface IBinaryMutationOperator
    {
        BinaryChromosome Mutate(BinaryChromosome chromosome);
    }
}
