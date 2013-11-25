using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetics.Generic
{
    public interface ISelector
    {
        /// <summary>
        /// Should create new instance of population and chromosomes inside it.
        /// </summary>
        /// <param name="population">Parent population.</param>
        /// <returns>New population after selection from parent population.</returns>
        Population Select(Population population);
    }
}
