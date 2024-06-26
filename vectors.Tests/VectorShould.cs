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
        private static List<Vector> MatrixToVectors(double[][] matrix) {
            var vectors = new List<Vector>();
            foreach (var array in matrix) vectors.Add(new Vector(array));
            return vectors;
        }

        private static Vector ArrayToVector(double[] array) {
            return new Vector(array);
        }

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
            termSum = TermSum([[1, 2, 3], [2, -1, -1, -1], [-1, -1]], [2, 0, 2, -1]);
            data.Add(new object[] { termSum.Item1, termSum.Item2 });
            return data;
        }

        public static IEnumerable<object[]> MultiplicationData()
        {
            (List<Vector>, Vector) FactorsProduct(double[][] factors, double[] product)
            {
                var factorVectors = new List<Vector>();
                foreach (var t in factors) factorVectors.Add(new Vector(t));
                var productVector = new Vector(product);
                return (factorVectors, productVector);
            }
            var data = new List<object[]>();
            var factorsProduct = FactorsProduct([[1, 2], [2]], [2, 2]);
            data.Add(new object[] { factorsProduct.Item1, factorsProduct.Item2 });
            factorsProduct = FactorsProduct([[1, 2, 3], [2, -1, -1, -1]], [2, -2, -3, -1]);
            data.Add(new object[] { factorsProduct.Item1, factorsProduct.Item2 });
            factorsProduct = FactorsProduct([[1, 2, 3], [2, -1, -1, -1], [-1, -1]], [-2, 2, -3, -1]);
            data.Add(new object[] { factorsProduct.Item1, factorsProduct.Item2 });
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

        [Theory]
        [MemberData(nameof(AdditionData))]
        public void AddVectorsWithOperator(List<Vector> terms, Vector expectedSum)
        {
            var sum = new Vector([0]);
            foreach(var term in terms) sum += term;
            Assert.Equal(sum, expectedSum);
        }

        [Theory]
        [MemberData(nameof(MultiplicationData))]
        public void MultiplyVectors(List<Vector> factors, Vector expectedProduct)
        {
            Assert.Equal(expectedProduct, Vector.Multiplication(factors));
        }

        [Theory]
        [MemberData(nameof(MultiplicationData))]
        public void MultiplyVectorsWithOperator(List<Vector> factors, Vector expectedProduct)
        {
            var product = new Vector([1]);
            foreach(var factor in factors) product *= factor;
            Assert.Equal(expectedProduct, product);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 6)]
        [InlineData(new double[] { 1, -1, 0 }, 0)]
        public void SumGetter(double[] array, double sum) {
            var vector = VectorShould.ArrayToVector(array);
            Assert.Equal(sum, vector.Sum);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 12, new double[] { 2, 4, 6 })]
        [InlineData(new double[] { 1, -2, 0 }, 4, new double[] { -4, 8, 0 })]
        public void SumSetter(double[] array, double sum, double[] expectedArray) {
            var vector = VectorShould.ArrayToVector(array);
            var expectedVector = VectorShould.ArrayToVector(expectedArray);
            
            vector.Sum = sum;

            Assert.Equal(expectedVector, vector);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, -3 }, 12)]
        [InlineData(new double[] { 1, -1, 0 }, 4)]
        public void SumSetterException(double[] array, double sum) {
            var vector = VectorShould.ArrayToVector(array);
            Assert.Throws<Exception>(() => vector.Sum = sum);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 6)]
        [InlineData(new double[] { 1, -1, 0 }, 0)]
        public void ProductGetter(double[] array, double product) {
            var vector = VectorShould.ArrayToVector(array);
            Assert.Equal(product, vector.Product);
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 12, new double[] { 1*1.25992104989, 2*1.25992104989, 3*1.25992104989 })]
        [InlineData(new double[] { 1, -2, 1 }, 4, new double[] { 1*(-1.25992104989), -2*(-1.25992104989), 1*(-1.25992104989) })]
        public void ProductSetter(double[] array, double product, double[] expectedArray) {
            var vector = VectorShould.ArrayToVector(array);
            var expectedVector = VectorShould.ArrayToVector(expectedArray);
            
            vector.Product = product;
            Assert.True(vector.Equals(expectedVector));
        }

        [Theory]
        [InlineData(new double[] { 1, 2, 3 }, 3.74165738677)]
        [InlineData(new double[] { 1, -1, 0 }, 1.41421356237)]
        [InlineData(new double[] { 0, 0, 0 }, 0)]
        public void MagnitudeGetter(double[] array, double magnitude) {
            var vector = VectorShould.ArrayToVector(array);
            Assert.Equal(magnitude, vector.Magnitude, 5);
        }

        [Theory]
        [InlineData(new double[] { 3, 4 }, 1, new double[] { 3*0.2, 4*0.2 })]
        public void MagnitudeSetter(double[] array, double magnitude, double[] expectedArray) {
            var vector = VectorShould.ArrayToVector(array);
            var expectedVector = VectorShould.ArrayToVector(expectedArray);
            
            vector.Magnitude = magnitude;
            Assert.True(vector == expectedVector);
        }

        [Theory]
        [InlineData(new double[] {1, 2, 3}, 2d, new double[] {2, 4, 6})]
        public void DoubleMultiplication(double[] array, double factor, double[] expectedArray) {
            var vector = VectorShould.ArrayToVector(array);
            var expectedVector = VectorShould.ArrayToVector(expectedArray);
            var product = Vector.Multiplication(vector, factor);
            Assert.True(product == expectedVector);
        }

        [Theory]
        [InlineData(new double[] {1, 2, 3}, new double[] {-1, -2, -3})]
        public void Negation(double[] array, double[] expectedArray) {
            var vector = VectorShould.ArrayToVector(array);
            var expectedVector = VectorShould.ArrayToVector(expectedArray);
            var negation = -vector;
            Assert.True(-vector == expectedVector);
        }
    }
}