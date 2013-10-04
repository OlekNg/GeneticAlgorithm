using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Reparation
{
    public interface IBinaryRepairer
    {
        BinaryChromosome Repair(BinaryChromosome chromosome);
    }
}
