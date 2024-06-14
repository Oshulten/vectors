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
        [InlineData(new double[]{ 1, 2, 3, 4, 5 }, 3, new double[]{ 1, 2, 3 })]
        [InlineData(new double[]{ 1, 2, 3 }, 5, new double[]{ 1, 2, 3, 0, 0 })]
        public void HaveASetterCountPropertyEqual(double[] array, int newCount, double[] expectedArray) {
            var vector = new Vector(array);
            vector.Count = newCount;
            Assert.Equal(new Vector(expectedArray), vector);
        }

        [Theory]
        [InlineData(new double[]{ 1, 2, 3, 4, 5 }, 3, new double[]{ 1, 2, 3, 4 })]
        [InlineData(new double[]{ 1, 2, 3 }, 5, new double[]{ 1, 2, 3, 0, 1 })]
        public void HaveASetterCountPropertyNotEqual(double[] array, int newCount, double[] resultingArray) {
            var vector = new Vector(array);
            vector.Count = newCount;
            Assert.NotEqual(vector, new Vector(resultingArray));
        }

        [Theory]
        [InlineData(new double[]{ 1, 2, 3, 4, 5 }, -1)]
        public void HaveASetterCountPropertyThrowsOnNegativeCount(double[] array, int newCount) {
            var vector = new Vector(array);
            Assert.Throws<ArgumentException>(() => vector.Count = newCount);
        }
    }
}