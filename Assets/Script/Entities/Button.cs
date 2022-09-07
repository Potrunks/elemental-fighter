using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Button", menuName = "Button")]
public class Button : ScriptableObject
{
    public string buttonName;
    public Sprite buttonSprite;
    public Color buttonColor;
}
