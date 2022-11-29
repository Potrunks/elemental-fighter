using UnityEngine;

namespace Assets.Script.Business
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Check if the gameobject touch the layer target depending of a circle radius.
        /// </summary>
        public static bool isTouchingLayer(this GameObject gameObject, float circleRadius, LayerMask layer)
        {
            return Physics2D.OverlapCircle(gameObject.transform.position, circleRadius, layer);
        }
    }
}
