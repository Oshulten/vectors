using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Vectors;
using Xunit.Abstractions;

namespace vectors.Tests
{
    public class ConstrainValueShould
    {
        [Theory]
        [InlineData(0, -1, 1, true)]
        [InlineData(-1, -1, 1, true)]
        [InlineData(1, -1, 1, true)]
        [InlineData(-2, -1, 1, false)]
        [InlineData(2, -1, 1, false)]
        [InlineData(0, 1, -1, true)]
        [InlineData(-1, 1, -1, true)]
        [InlineData(1, 1, -1, true)]
        [InlineData(-2, 1, -1, false)]
        [InlineData(2, 1, -1, false)]
        [InlineData(-1, 0, 0, false)]
        [InlineData(0, 0, 0, true)]
        [InlineData(1, 0, 0, false)]
        public void TellIfAValueIsInRange(double value, double min, double max, bool inRange)
        {
            Assert.Equal(inRange, ConstrainValue.InRange(value, min, max));
        }
        [Theory]
        [InlineData(0, -1, 1, 0)]
        [InlineData(1, -1, 1, 1)]
        [InlineData(-1, -1, 1, -1)]
        [InlineData(1.5, -1, 1, -0.5)]
        [InlineData(-1.5, -1, 1, 0.5)]
        [InlineData(-3, -1, 1, -1)]
        [InlineData(3, -1, 1, -1)]
        public void Cycle(double value, double min, double max, double expectedValue)
        {
            Assert.Equal(expectedValue, ConstrainValue.Cycle(value, min, max), 1);
        }
        [Theory]
        [InlineData(-1d, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(null, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(null, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1d, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(null, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(null, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        public void ThrowExceptionOnConstrainGuard(double? min, double? max, ConstraintType lowerConstraint, ConstraintType higherConstraint)
        {
            Assert.Throws<ArgumentException>(() => ConstrainValue.ConstrainGuard(min, max, lowerConstraint, higherConstraint));
        }

        [Theory]
        //in range
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(0d, 0d, null, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(0d, 0d, -1d, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(0d, 0d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(0d, 0d, null, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, null, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(0d, 0d, null, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(0d, 0d, null, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(0d, 0d, null, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        //upper endpoint
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(1d, 1d, null, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(1d, 1d, -1d, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(1d, 1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(1d, 1d, null, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, null, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(1d, 1d, null, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(1d, 1d, null, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(1d, 1d, null, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        //lower endpoint
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1d, -1d, null, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1d, -1d, null, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        //in range one magnitude lower
        [InlineData(-1.5d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1d, -1d, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1.5d, 0.5d, -1d, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1.5d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1.5d, -1.5d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1.5d, -1d, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1.5d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1.5d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1d, null, 1d, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, 0.5d, null, 1d, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, null, 1d, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1.5d, 0.5d, null, 1d, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1.5d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1.5d, -1.5d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1.5d, null, 1d, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1.5d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1d, -1d, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, -1d, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, -1d, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1.5d, 0.5d, -1d, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1.5d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1.5d, -1.5d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1.5d, -1d, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

        [InlineData(-1.5d, -1d, null, null, ConstraintType.Cap, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1d, null, null, ConstraintType.Cap, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1d, null, null, ConstraintType.Cap, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, null, null, ConstraintType.Cycle, ConstraintType.Cap)]
        [InlineData(-1.5d, 0.5d, null, null, ConstraintType.Cycle, ConstraintType.Cycle)]
        [InlineData(-1.5d, 0.5d, null, null, ConstraintType.Cycle, ConstraintType.Unconstrained)]
        [InlineData(-1.5d, -1.5d, null, null, ConstraintType.Unconstrained, ConstraintType.Cap)]
        [InlineData(-1.5d, -1.5d, null, null, ConstraintType.Unconstrained, ConstraintType.Cycle)]
        [InlineData(-1.5d, -1.5d, null, null, ConstraintType.Unconstrained, ConstraintType.Unconstrained)]

       
        public void Constraint(double value, double expected, double? min, double? max, ConstraintType lowerConstraint, ConstraintType higherConstraint)
        {
            try {
                Assert.Equal(expected, ConstrainValue.Constrain(value, min, max, lowerConstraint, higherConstraint), 1);
            }
            catch (ArgumentException) {
            }
        }
    }
}