using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Vectors;

namespace vectors.Tests
{
    public class ConstrainedVectorShould
    {
        [Fact]
        public void Test1()
        {
            var vector = new ConstrainedVector([-1, -0.5, 0, 0.5, 1, 1.5, 2, 2.5], [
                new Constraint(0, 1, ConstraintType.Cycle, ConstraintType.Cycle),
            ]);
        }
    }
}