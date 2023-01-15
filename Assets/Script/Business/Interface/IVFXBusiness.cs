using UnityEngine;

namespace Assets.Script.Business
{
    public interface IVFXBusiness
    {
        /// <summary>
        /// Clear the tween effect of image component of the child game object from a parent gameobject
        /// </summary>
        /// <param name="gameObjectParent">GameObject parent with the child game object targeted</param>
        /// <param name="nameChildGameObjectWithImageComponent">Name of the child game object with the image component targeted</param>
        void ClearTweenEffectOfImageComponent(GameObject gameObjectParent, string nameChildGameObjectWithImageComponent);
    }
}
