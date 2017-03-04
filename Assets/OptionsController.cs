using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

    public Button back, music, sound;
    public Sprite musicOn, musicOff, soundOn, soundOff;

    void Start () {

        Debug.Log(PlayerPrefs.GetString("Music"));

        back.GetComponent<Button>().onClick.AddListener(returnToMenu);
        music.GetComponent<Button>().onClick.AddListener(musicController);
        sound.GetComponent<Button>().onClick.AddListener(soundController);

        if (PlayerPrefs.GetString("Music") == "ON")
            music.image.sprite = musicOn;
        else
            music.image.sprite = musicOff;

        if (PlayerPrefs.GetString("Sound") == "ON")
            sound.image.sprite = soundOn;
        else
            sound.image.sprite = soundOff;
    }
    
    private void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void musicController()
    {
        if (PlayerPrefs.GetString("Music") == "ON")
        {
            PlayerPrefs.SetString("Music", "OFF");
            music.image.sprite = musicOff;
        }
        else
        {
            PlayerPrefs.SetString("Music", "ON");
            music.image.sprite = musicOn;
        }
    }

    private void soundController()
    {
        if (PlayerPrefs.GetString("Sound") == "ON")
        {
            PlayerPrefs.SetString("Sound", "OFF");
            sound.image.sprite = soundOff;
        }
        else
        {
            PlayerPrefs.SetString("Sound", "ON");
            sound.image.sprite = soundOn;
        }
    }

}
