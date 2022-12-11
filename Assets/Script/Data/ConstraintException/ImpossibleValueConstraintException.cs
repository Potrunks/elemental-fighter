using System;

namespace Assets.Script.Data.ConstraintException
{
    public class ImpossibleValueConstraintException : Exception
    {
        public ImpossibleValueConstraintException() { }

        public ImpossibleValueConstraintException(string message, float indexPlayer) : base(String.Format(message, indexPlayer)) { }
    }
}
