using UnityEngine;
using TMPro;
using System.Collections;

public class ReadyFightScript : MonoBehaviour
{
    public static ReadyFightScript instance;
    private TextMeshProUGUI readyFightText;
    [HideInInspector]
    public bool fightIsStarted = false;

    [Header("Audio")]
    public AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        readyFightText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StopGameForSeconds());
    }

    IEnumerator StopGameForSeconds()
    {
        readyFightText.text = "Ready...";
        audioManager.Play("Ready");
        yield return new WaitForSeconds(2);
        readyFightText.text = "Fight !!!";
        audioManager.Play("Fight");
        yield return new WaitForSeconds(1);
        readyFightText.text = "";
        GameManager.instance.timeIsActivated = true;
        fightIsStarted = true;
    }
}
