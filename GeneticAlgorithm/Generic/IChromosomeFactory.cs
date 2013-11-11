﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IChromosomeFactory
    {
        IChromosome Create();
    }
}
