using Assets.Script.Data.ConstraintException;
using Assets.Script.Data.Reference;
using UnityEngine;

namespace Assets.Script.Business
{
    public static class SpriteRendererExtension
    {
        public static void ChangeColorByIndexPlayer(this SpriteRenderer spriteRendererToChangeColor, int indexPlayer)
        {
            switch (indexPlayer)
            {
                case 0:
                    spriteRendererToChangeColor.material.color = Color.white;
                    break;

                case 1:
                    spriteRendererToChangeColor.material.color = new Color32(133, 136, 253, 255);
                    break;

                case 2:
                    spriteRendererToChangeColor.material.color = new Color32(141, 253, 134, 255);
                    break;

                case 3:
                    spriteRendererToChangeColor.material.color = new Color32(245, 253, 133, 255);
                    break;

                default:
                    throw new ImpossibleValueConstraintException(ImpossibleValueConstraintExceptionMessageReference.PLAYER_INDEX, indexPlayer);
            }
        }
    }
}
