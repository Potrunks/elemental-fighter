using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{
    [SerializeField]
    private Button buttonEntities;

    private Image imageComponent;

    void Start()
    {
        this.imageComponent = GetComponent<Image>();

        this.gameObject.name = buttonEntities.buttonName;
        this.imageComponent.sprite = buttonEntities.buttonSprite;
        this.imageComponent.color = buttonEntities.buttonColor;
    }
}
