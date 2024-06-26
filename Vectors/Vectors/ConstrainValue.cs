using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vectors
{
    public enum ConstraintType
    {
        Cap,
        Cycle,
        Unconstrained
    }

    public class Constraint {
        public double? Minimum { get; set;}
        public double? Maximum { get; set;}
        public ConstraintType LowerConstraint { get; set; }
        public ConstraintType UpperConstraint { get; set; }
        public Constraint(double? min, double? max, ConstraintType lowerConstraint, ConstraintType upperConstraint) {
            ConstrainValue.ConstrainGuard(min, max, lowerConstraint, upperConstraint);
            this.Minimum = min;
            this.Maximum = max;
            this.LowerConstraint = lowerConstraint;
            this.UpperConstraint = upperConstraint;
        }
    }

    public static class ConstrainValue
    {
        private static (double, double) SortMinMax(double min, double max)
        {
            return (min > max) ? (max, min) : (min, max);
        }
        public static bool InRange(double value, double min, double max)
        {
            (min, max) = SortMinMax(min, max);
            return (value >= min && value <= max);
        }
        public static double Cycle(double value, double min, double max)
        {
            (min, max) = SortMinMax(min, max);
            double mod(double x, double m)
            {
                return (x % m + m) % m;
            }
            return InRange(value, min, max) ? value : mod(value - min, max - min) + min;
        }
        public static void ConstrainGuard(double? min, double? max, ConstraintType lowerConstraint = ConstraintType.Unconstrained, ConstraintType upperConstraint = ConstraintType.Unconstrained)
        {
            if ((min is null || max is null) && (lowerConstraint == ConstraintType.Cycle || upperConstraint == ConstraintType.Cycle)) {
                throw new ArgumentException("Cycle constraint cannot be used in combination with null min or max argument");
            }
            if (min is null && max is null) throw new ArgumentException("Both min and max cannot be null");
            if (max is null && upperConstraint == ConstraintType.Cap) throw new ArgumentException("Use of higher ConstraintType.Cap cannot be combined with a null value of max");
            if (min is null && lowerConstraint == ConstraintType.Cap) throw new ArgumentException("Use of lower ConstraintType.Cap cannot be combined with a null value of min");
        }
        public static double Constrain(double value, Constraint constraint) {
            return ConstrainValue.Constrain(value, constraint.Minimum, constraint.Maximum, constraint.LowerConstraint, constraint.UpperConstraint);
        }
        public static double Constrain(double value, double? min, double? max, ConstraintType lowerConstraint = ConstraintType.Unconstrained, ConstraintType upperConstraint = ConstraintType.Unconstrained)
        {
            ConstrainGuard(min, max, lowerConstraint, upperConstraint);
            if (min is null && max is null) return value;
            if (min is not null && max is not null)
            {
                if (value > max)
                {
                    return upperConstraint switch
                    {
                        ConstraintType.Cap => (double)max,
                        ConstraintType.Cycle => Cycle(value, (double)min, (double)max),
                        _ => value,
                    };
                }
                if (value < min)
                {
                    return lowerConstraint switch
                    {
                        ConstraintType.Cap => (double)min,
                        ConstraintType.Cycle => Cycle(value, (double)min, (double)max),
                        _ => value,
                    };
                }
            }
            if (max is not null && min is null)
            {
                if (value > max)
                {
                    return upperConstraint switch
                    {
                        ConstraintType.Cap => (double)max,
                        _ => value,
                    };
                }
            }
            if (min is not null && max is null)
            {
                if (value < min)
                {
                    return lowerConstraint switch
                    {
                        ConstraintType.Cap => (double)min,
                        _ => value,
                    };
                }
            }
            return value;
        }
    }
}