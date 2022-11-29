using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Business
{
    public interface IPhysicsBusiness
    {
        /// <summary>
        /// Apply rigidbody contraint 2D list.
        /// The only rigidbody constraint 2D available is PositionX, PositionY and Rotation.
        /// If it's this option, None is set by default.
        /// </summary>
        public RigidbodyConstraints2D ApplyRigidbodyConstraint2D(IEnumerable<RigidbodyConstraints2D> rigidbodyConstraints2DListToApply);
    }
}
