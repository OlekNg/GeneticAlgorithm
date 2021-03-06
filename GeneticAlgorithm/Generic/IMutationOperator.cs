﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface IMutationOperator<T>
    {
        T Mutate(Chromosome<T> c);
    }
}
