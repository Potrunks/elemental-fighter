using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Script.Business.Interface;
using Assets.Script.Business.Implementation;

public class CursorDetection : MonoBehaviour
{
    [SerializeField]
    private GraphicRaycaster graphicRaycaster;
    [SerializeField]
    private PointerEventData pointerEventData = new PointerEventData(null);
    public GameObject characterUnderCursor;
    public GameObject playerSlot;
    public int indexPlayer;
    public GameObject token;
    public bool cursorHasToken;

    private ITokenBusiness tokenBusiness = new TokenBusiness();
    private IVFXBusiness vFXBusiness = new VFXBusiness();
    private ICharacterBusiness characterBusiness = new CharacterBusiness();

    void Start()
    {
        cursorHasToken = true;
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    void Update()
    {
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        tokenBusiness.FollowCursor(cursorHasToken, token, this.gameObject);

        if (cursorHasToken == true)
        {
            if (raycastResults.Count > 0)
            {
                if (characterUnderCursor != raycastResults[0].gameObject && raycastResults[0].gameObject.name != "SelectionBackButton")
                {
                    if (characterUnderCursor != null)
                    {
                        vFXBusiness.ClearTweenEffectOfImageComponent(characterUnderCursor, "Border");
                    }
                    characterUnderCursor = raycastResults[0].gameObject;
                    characterUnderCursor.transform.Find("Border").GetComponent<Image>().color = Color.white;
                    characterUnderCursor.transform.Find("Border").GetComponent<Image>().DOColor(Color.red, 1).SetLoops(-1);
                    characterBusiness.ShowCharacterInPlayerSlot(playerSlot, characterUnderCursor);
                }
                else if (raycastResults[0].gameObject.name == "SelectionBackButton")
                {
                    characterUnderCursor = raycastResults[0].gameObject;
                }
            }
            else
            {
                if (characterUnderCursor != null && characterUnderCursor.name != "SelectionBackButton")
                {
                    vFXBusiness.ClearTweenEffectOfImageComponent(characterUnderCursor, "Border");
                }
                characterUnderCursor = null;
                characterBusiness.ShowCharacterInPlayerSlot(playerSlot, null);
            }
        }
    }
}
