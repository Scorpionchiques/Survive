using UnityEngine;
using UnityEngine.UI;

public class ButtonsSoundController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        var Buttons = FindObjectsOfType(typeof(Button));
        foreach (Button button in Buttons)
        {
            button.onClick.AddListener(clickSound);
        }
    }

    private void clickSound()
    {
        if (PlayerPrefs.GetString("Sound") == "ON")
            GetComponent<AudioSource>().Play();
    }
    
}
