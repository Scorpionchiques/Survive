using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {

    public Button back, music, sound;
    public Sprite musicOn, musicOff, soundOn, soundOff;

    void Start () {

        back.GetComponent<Button>().onClick.AddListener(returnToMenu);
        music.GetComponent<Button>().onClick.AddListener(musicController);
        sound.GetComponent<Button>().onClick.AddListener(soundController);

        if (PlayerPrefs.GetString("Music") == "ON")
            setMusicOn();
        else
            setMusicOff();
        if (PlayerPrefs.GetString("Sound") == "ON")
            setSoundOn();
        else
            setSoundOff();
    }
    
    private void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void musicController()
    {
        if (PlayerPrefs.GetString("Music") == "ON")
        {
            setMusicOff();
        }
        else
        {
            setMusicOn();
        }
    }

    private void soundController()
    {
        if (PlayerPrefs.GetString("Sound") == "ON")
        {
            setSoundOff();
        }
        else
        {
            setSoundOn();
        }
    }

    private void setMusicOn()
    {
        PlayerPrefs.SetString("Music", "ON");
        music.image.sprite = musicOn;
        music.GetComponentInChildren<Text>().text = "MUSIC: ON";
    }

    private void setMusicOff()
    {
        PlayerPrefs.SetString("Music", "OFF");
        music.image.sprite = musicOff;
        music.GetComponentInChildren<Text>().text = "MUSIC: OFF";
    }

    private void setSoundOn()
    {
        PlayerPrefs.SetString("Sound", "ON");
        sound.image.sprite = soundOn;
        sound.GetComponentInChildren<Text>().text = "SOUND: ON";
    }

    private void setSoundOff()
    {
        PlayerPrefs.SetString("Sound", "OFF");
        sound.image.sprite = soundOff;
        sound.GetComponentInChildren<Text>().text = "SOUND: OFF";
    }
}
