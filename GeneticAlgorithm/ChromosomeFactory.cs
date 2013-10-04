using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics
{
    public abstract class ChromosomeFactory
    {
        public abstract IChromosome Create();
    }
}
