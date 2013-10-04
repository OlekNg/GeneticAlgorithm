using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Selection
{
    public interface ISelector
    {
        List<IChromosome> Select(List<IChromosome> population);
    }
}
