using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterMenuScript : MonoBehaviour
{
    [Header("Character Preview Settings")]
    private List<CharacterPreview> characterPreviewList;
    public GameObject characterPreviewPrefab;

    void Start()
    {
        characterPreviewList = SelectCharacterManager.instance.characterPreviewList;
        SpawnAllCharacterPreview();
    }

    /// <summary>
    /// Allow to spawn all character preview in a cell of character preview prefab
    /// </summary>
    private void SpawnAllCharacterPreview()
    {
        foreach (CharacterPreview characterPreview in characterPreviewList)
        {
            SpawnOneCharacterPreview(characterPreview);
        }
    }

    /// <summary>
    /// Allow to spawn one character preview in a cell of character preview prefab
    /// </summary>
    /// <param name="characterPreview">Scriptable object character preview</param>
    private void SpawnOneCharacterPreview(CharacterPreview characterPreview)
    {
        GameObject characterPreviewGameObject = Instantiate(characterPreviewPrefab, transform);

        CharacterPreviewData characterPreviewData = characterPreviewGameObject.GetComponent<CharacterPreviewData>();
        Image characterPreviewImage = characterPreviewGameObject.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI characterPreviewName = characterPreviewGameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();

        characterPreviewImage.sprite = characterPreview.characterSprite;
        characterPreviewName.text = characterPreview.characterName;
        characterPreviewGameObject.name = characterPreview.characterName;
        characterPreviewData.characterModel = characterPreview.characterPrefab;
    }
}
