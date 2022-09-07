using Assets.Script.Business.Interface;
using UnityEngine;

namespace Assets.Script.Business.Implementation
{
    internal class ElementalBusiness : IElementalBusiness
    {
        public void SetElementalColorByPlayerIndex(GameObject elementalGameObject, int casterPlayerIndex)
        {
            SpriteRenderer elementalSprite = elementalGameObject.GetComponent<SpriteRenderer>();
            switch (casterPlayerIndex)
            {
                case 1:
                    elementalSprite.color = new Color32(133, 136, 253, 255);
                    break;

                case 2:
                    elementalSprite.color = new Color32(141, 253, 134, 255);
                    break;

                case 3:
                    elementalSprite.color = new Color32(245, 253, 133, 255);
                    break;

                default:
                    elementalSprite.color = Color.white;
                    break;
            }
        }
    }
}
