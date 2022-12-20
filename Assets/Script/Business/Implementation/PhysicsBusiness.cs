using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Business
{
    public class PhysicsBusiness : IPhysicsBusiness
    {
        public RigidbodyConstraints2D ApplyRigidbodyConstraint2D(IEnumerable<RigidbodyConstraints2D> rigidbodyConstraints2DListToApply)
        {
            if (rigidbodyConstraints2DListToApply == null)
            {
                return RigidbodyConstraints2D.None;
            }
            IEnumerable<RigidbodyConstraints2D> validRigidbodyConstraints2DList = rigidbodyConstraints2DListToApply.Except(new List<RigidbodyConstraints2D>() { RigidbodyConstraints2D.None, RigidbodyConstraints2D.FreezePosition, RigidbodyConstraints2D.FreezeAll }).Distinct();
            if (validRigidbodyConstraints2DList.Any())
            {
                RigidbodyConstraints2D rigidbodyConstraints2D = validRigidbodyConstraints2DList.First();
                foreach (RigidbodyConstraints2D constraints in validRigidbodyConstraints2DList.Skip(1))
                {
                    rigidbodyConstraints2D = rigidbodyConstraints2D | constraints;
                }
                return rigidbodyConstraints2D;
            }
            return RigidbodyConstraints2D.None;
        }
    }
}
