using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genetics.Generic;

namespace Genetics.Specialized
{
    public class MultiPointCrossover : ICrossoverOperator<List<bool>>
    {
        private const int DEFAULT_POINTS = 2;

        /// <summary>
        /// To draw a slice points.
        /// </summary>
        private Random _randomizer;

        private int _points;

        public MultiPointCrossover()
            : this(DEFAULT_POINTS) { }

        public MultiPointCrossover(int points)
        {
            _randomizer = new Random();
            _points = points;
        }

        Tuple<List<bool>, List<bool>> ICrossoverOperator<List<bool>>.Crossover(List<bool> g1, List<bool> g2)
        {
            int length = g1.Count;

            List<bool>[] newGenotypes = new List<bool>[2];
            newGenotypes[0] = new List<bool>(length);
            newGenotypes[1] = new List<bool>(length);

            // Draw slice points
            List<int> slicePoints = new List<int>();
            for (int i = 0; i < _points; i++)
                slicePoints.Add(_randomizer.Next(0, length));

            // Add end point
            slicePoints.Add(g1.Count);

            slicePoints = slicePoints.OrderBy(x => x).ToList();

            int lastPoint = 0;
            for (int i = 0; i < slicePoints.Count; i++)
            {
                int point = slicePoints[i];

                newGenotypes[i % 2].AddRange(g1.GetRange(lastPoint, point - lastPoint));
                newGenotypes[(i + 1) % 2].AddRange(g2.GetRange(lastPoint, point - lastPoint));

                lastPoint = point;
            }

            return new Tuple<List<bool>, List<bool>>(newGenotypes[0], newGenotypes[1]);
        }
    }
}
