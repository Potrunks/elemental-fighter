using System;
using System.Collections.Generic;

namespace Assets.Script.Data.ConstraintException
{
    public class ImpossibleValueConstraintException : Exception
    {
        public ImpossibleValueConstraintException(string message, float value) : base(string.Format(message, value)) { }

        public ImpossibleValueConstraintException(string message, int value1, int value2) : base(string.Format(message, value1, value2)) { }

        public ImpossibleValueConstraintException(string message, List<int> values) : base(message + string.Join(", ", values)) { }
    }
}
