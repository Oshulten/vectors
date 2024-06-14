using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class Vector : List<double>, IEquatable<Vector>
    {
        private static double EqualityPrecision = 3;
        public Vector() : base() { }
        public Vector(double[] values) : base(values) { }
        public Vector(int capacity) : base(capacity) { }
        public new int Count { get => base.Count; set => this.AdjustCount(value); }

        public static Vector operator +(Vector v1, Vector v2) => Vector.Addition([v1, v2]);
        public static Vector operator *(Vector v1, Vector v2) => Vector.Multiplication([v1, v2]);
        public static bool operator ==(Vector v1, Vector v2) => v1.Equals(v2);
        public static bool operator !=(Vector v1, Vector v2) => !v1.Equals(v2);

        public static Vector Addition(List<Vector> vectors)
        {
            var matchedVectors = Vector.MatchVectors(vectors, 0d);
            var dimension = matchedVectors[0].Count;
            var sumVector = new Vector(dimension);
            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                foreach (var vector in matchedVectors) sum += vector[i];
                sumVector.Add(sum);
            }
            return sumVector;
        }

        public static Vector Multiplication(List<Vector> vectors)
        {
            var matchedVectors = Vector.MatchVectors(vectors, 1d);
            var dimension = matchedVectors[0].Count;
            var productVector = new Vector(dimension);
            for (int i = 0; i < dimension; i++)
            {
                double product = 1;
                foreach (var vector in matchedVectors) product *= vector[i];
                productVector.Add(product);
            }
            return productVector;
        }

        public double Product
        {
            get
            {
                double product = 1;
                foreach (double value in this) product *= value;
                return product;
            }
            set
            {
                if (this.Product == 0d && value != 0d) throw new Exception("A vector of product 0 cannot be refactored");
                double k = Math.Pow((value / this.Product), 1d / this.Count);
                for (int i = 0; i < this.Count; i++) this[i] *= k;
            }
        }

        public double Sum
        {
            get => (from value in this select value).Sum();
            set
            {
                if (this.Sum == 0d && value != 0d) throw new Exception("A vector of sum 0 cannot be resummed");
                double k = value / this.Sum;
                for (int i = 0; i < this.Count; i++) this[i] *= k;
            }
        }

        public override string ToString()
        {
            return $"[{String.Join<double>(", ", this)}]";
        }

        //A cheeky comment
        private void AdjustCount(int newCount, double fillValue = 0d)
        {
            if (newCount < 0)
            {
                throw new ArgumentException("Count must be greater or equal to 0");
            }
            if (this.Count > newCount)
            {
                this.RemoveRange(newCount, this.Count - newCount);
            }
            if (this.Count < newCount) while (this.Count < newCount) this.Add(fillValue);
        }
        public static List<Vector> MatchVectors(List<Vector> vectors, double fillValue = 0d)
        {
            var vectorsCopy = new List<Vector>(vectors);
            int maxCount =
                (from vector in vectorsCopy
                 select vector.Count).Max();
            vectorsCopy.ForEach((vector) => vector.AdjustCount(maxCount, fillValue));
            return vectorsCopy;
        }

        public bool Equals(Vector? other)
        {
            if (other is null) return false;
            if (this.Count != other.Count) return false;
            for (int i = 0; i < this.Count; i++)
                if (Math.Abs(this[i] - other[i]) > Math.Pow(10, -Vector.EqualityPrecision)) return false;
            return true;
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null)
                return false;

            Vector vector = (Vector)obj;
            if (vector is null)
                return false;
            else
                return Equals(vector);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}