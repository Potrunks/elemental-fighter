using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Business
{
    public class VFXBusiness : IVFXBusiness
    {
        public void ClearTweenEffectOfImageComponent(GameObject gameObjectParent, string nameChildGameObjectWithImageComponent)
        {
            gameObjectParent.transform.Find(nameChildGameObjectWithImageComponent).GetComponent<Image>().DOKill();
            gameObjectParent.transform.Find(nameChildGameObjectWithImageComponent).GetComponent<Image>().color = Color.clear;
        }
    }
}
