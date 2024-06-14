using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public class ConstrainedVector : Vector
    {
        private IList<Constraint> constraints;
        public IList<Constraint> Constraints { get => this.constraints; set => this.constraints = value; }
        public ConstrainedVector(double[] values, IList<Constraint> constraints) : base(values)
        {
            Constraints = constraints.Count > 0
                ? constraints
                : [new Constraint(null, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)];
            this.Constrain();
        }
        private void Constrain()
        {
            for (int i = 0; i < this.Count; i++)
            {
                int j = (i >= this.Constraints.Count) ? this.Constraints.Count - 1 : i;
                base[i] = ConstrainValue.Constrain(this[i], this.Constraints[j]);
            }
        }
        // public new double this[int i]
        // {
        //     get
        //     {
        //         int j = i >= this.Constraints.Count ? i : this.Constraints.Count - 1;
        //         return ConstrainValue.Constrain(base[i], Constraints[j]);
        //     }
        //     set
        //     {
        //         int j = i >= this.Constraints.Count ? i : this.Constraints.Count - 1;
        //         this[i] = ConstrainValue.Constrain(value, this.Constraints[j]);
        //     }
        // }
    }
}