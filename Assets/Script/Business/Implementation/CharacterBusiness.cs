using Assets.Script.Business.Interface;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Business.Implementation
{
    internal class CharacterBusiness : ICharacterBusiness
    {
        private static readonly System.Random random = new System.Random();

        private IVFXBusiness vFXBusiness = new VFXBusiness();

        public void ConfirmCharacter(GameObject playerslot, GameObject characterUnderCursor)
        {
            vFXBusiness.ClearTweenEffectOfImageComponent(characterUnderCursor, "Border");
            playerslot.transform.DOPunchPosition(Vector3.down * 3, .3f, 10, 1);
        }

        public GameObject GetRandomCharacter()
        {
            List<CharacterPreview> characterPreviewListWoRandom = SelectCharacterManager.instance.characterPreviewList.Where(cp => cp.characterPrefab != null).ToList();
            int randomInt = random.Next(characterPreviewListWoRandom.Count);
            return characterPreviewListWoRandom.ElementAt(randomInt).characterPrefab;
        }

        public Vector2 MoveCharacter(Vector2 inputMoveValue, float moveSpeed, Rigidbody2D rigidbodyToMove, float smoothTime)
        {
            Vector3 reference = Vector3.zero;
            float horizontalMovement = inputMoveValue.x * moveSpeed * Time.deltaTime;
            return Vector3.SmoothDamp(rigidbodyToMove.velocity,
                                      new Vector2(horizontalMovement, rigidbodyToMove.velocity.y),
                                      ref reference,
                                      smoothTime);
        }

        public void SetSpriteRendererColorByIndexPlayer(int playerIndex, SpriteRenderer spriteRenderer)
        {
            switch (playerIndex)
            {
                case 1:
                    spriteRenderer.material.color = new Color32(133, 136, 253, 255);
                    break;

                case 2:
                    spriteRenderer.material.color = new Color32(141, 253, 134, 255);
                    break;

                case 3:
                    spriteRenderer.material.color = new Color32(245, 253, 133, 255);
                    break;

                default:
                    spriteRenderer.material.color = Color.white;
                    break;
            }
        }

        public void ShowCharacterInPlayerSlot(GameObject playerSlot, GameObject characterUnderPlayerCursor)
        {
            Transform playerSlotImageTransform = playerSlot.transform.Find("Image");
            Image playerSlotImage = playerSlotImageTransform.GetComponent<Image>();
            playerSlotImage.sprite = null;
            playerSlotImage.color = Color.clear;
            TextMeshProUGUI playerSlotCharacterName = playerSlot.transform.Find("CharacterNameSelected").GetComponent<TextMeshProUGUI>();
            playerSlotCharacterName.text = "No player selected";

            if (characterUnderPlayerCursor != null)
            {
                Image characterUnderPlayerCursorImage = characterUnderPlayerCursor.transform.Find("Image").GetComponent<Image>();
                Sequence s = DOTween.Sequence();
                s.Append(playerSlotImageTransform.DOLocalMoveX(-100, 0.05f).SetEase(Ease.OutCubic));
                s.AppendCallback(() => playerSlotImage.sprite = characterUnderPlayerCursorImage.sprite);
                s.AppendCallback(() => playerSlotImage.color = Color.white);
                s.Append(playerSlotImageTransform.DOLocalMoveX(100, 0));
                s.Append(playerSlotImageTransform.DOLocalMoveX(0, 0.05f).SetEase(Ease.OutCubic));
                TextMeshProUGUI characterUnderPlayerCursorText = characterUnderPlayerCursor.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                playerSlotCharacterName.text = characterUnderPlayerCursorText.text;
            }
        }
    }
}
