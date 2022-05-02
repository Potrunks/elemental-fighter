using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float volumeMainTheme;
    public int victoryPointCondition;
    public int timeCondition;
    public bool timeIsActivated;
    public float currentTime;
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
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (currentTime > 0 && timeIsActivated)
        {
            currentTime -= Time.deltaTime % 60;
        }
    }
}
