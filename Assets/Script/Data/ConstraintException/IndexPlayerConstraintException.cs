using System;

namespace Assets.Script.Data.ConstraintException
{
    public class IndexPlayerConstraintException : Exception
    {
        public IndexPlayerConstraintException() { }

        public IndexPlayerConstraintException(int indexPlayer) : base(String.Format("Unknown index player : {0}", indexPlayer)) { }
    }
}
