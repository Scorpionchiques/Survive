using UnityEngine;

public class MenuMusicContoller : MonoBehaviour
{
    private static MenuMusicContoller instance = null;
    private bool isPlaying;

    public static MenuMusicContoller Instance
    {
        get {
            return instance;
        }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.GetString("Music") == "ON")
        {
            GetComponent<AudioSource>().Play();
            isPlaying = true;
        }
        else
            isPlaying = false;

    }

    void Update()
    {
        if (isPlaying && PlayerPrefs.GetString("Music") == "OFF")
        {
            GetComponent<AudioSource>().Pause();
            isPlaying = false;
        }
        if (!isPlaying && PlayerPrefs.GetString("Music") == "ON")
        {
            GetComponent<AudioSource>().Play();
            isPlaying = true;
        }
            
    }
}
