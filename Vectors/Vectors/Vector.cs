using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class Vector : List<double>
    {
        public Vector() : base() { }
        public Vector(double[] values) : base(values) { }
        public new int Count { get => base.Count; set => this.AdjustCount(value); }

        public override string ToString() {
            return $"[{String.Join<double>(", ", this)}]";
        }

        private void AdjustCount(int newCount)
        {
            if (newCount < 0)
            {
                throw new ArgumentException("Count must be greater or equal to 0");
            }
            if (this.Count > newCount)
            {
                this.RemoveRange(newCount, this.Count - newCount);
            }
            if (this.Count < newCount) while (this.Count < newCount) this.Add(0d);
        }
    }
}