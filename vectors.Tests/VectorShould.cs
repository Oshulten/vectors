using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Vectors;

namespace vectors.Tests
{
    public class VectorShould
    {
        public static IEnumerable<object[]> AdditionData()
        {
            (List<Vector>, Vector) TermSum(double[][] terms, double[] sum)
            {
                var termVectors = new List<Vector>();
                foreach (var t in terms) termVectors.Add(new Vector(t));
                var sumVector = new Vector(sum);
                return (termVectors, sumVector);
            }
            var data = new List<object[]>();
            var termSum = TermSum([[1, 2], [2]], [3, 2]);
            data.Add(new object[] { termSum.Item1, termSum.Item2 });
            termSum = TermSum([[1, 2, 3], [2, -1, -1, -1]], [3, 1, 2, -1]);
            data.Add(new object[] { termSum.Item1, termSum.Item2 });
            return data;
        }

        [Fact]
        public void BehaveAsAList()
        {
            var vector = new Vector();
            Assert.IsType<Vector>(vector);
            Assert.IsAssignableFrom<List<double>>(vector);
            Assert.Empty(vector);
            vector.Add(35d);
            Assert.Single(vector);
            Assert.Throws<ArgumentOutOfRangeException>(() => vector[1]);
        }
        [Theory]
        [InlineData(new double[] { 1, 2, 3, 4, 5 }, 3, new double[] { 1, 2, 3 })]
        [InlineData(new double[] { 1, 2, 3 }, 5, new double[] { 1, 2, 3, 0, 0 })]
        public void HaveASetterCountPropertyEqual(double[] array, int newCount, double[] expectedArray)
        {
            var vector = new Vector(array);
            vector.Count = newCount;
            Assert.Equal(new Vector(expectedArray), vector);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3, 4, 5 }, 3, new double[] { 1, 2, 3, 4 })]
        [InlineData(new double[] { 1, 2, 3 }, 5, new double[] { 1, 2, 3, 0, 1 })]
        public void HaveASetterCountPropertyNotEqual(double[] array, int newCount, double[] resultingArray)
        {
            var vector = new Vector(array);
            vector.Count = newCount;
            Assert.NotEqual(vector, new Vector(resultingArray));
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3, 4, 5 }, -1)]
        public void HaveASetterCountPropertyThrowsOnNegativeCount(double[] array, int newCount)
        {
            var vector = new Vector(array);
            Assert.Throws<ArgumentException>(() => vector.Count = newCount);
        }

        [Theory]
        [MemberData(nameof(AdditionData))]
        public void AddVectors(List<Vector> terms, Vector expectedSum)
        {
            Assert.Equal(Vector.Addition(terms), expectedSum);
        }
    }
}