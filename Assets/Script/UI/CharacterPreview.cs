using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character Preview", menuName = "Character Preview")]
public class CharacterPreview : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public GameObject characterPrefab;
}
