using UnityEngine;

public class MenuMusicContoller : MonoBehaviour
{
    private static MenuMusicContoller instance = null;
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
    }
}
