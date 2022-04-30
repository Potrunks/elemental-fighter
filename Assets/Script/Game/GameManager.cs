using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float volumeMainTheme;
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
}
