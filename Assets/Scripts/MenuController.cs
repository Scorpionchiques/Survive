using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Button play, options, help, exit, share, rate;

	// Use this for initialization
	void Start () {
        play.GetComponent<Button>().onClick.AddListener(onPlayPressed);
        options.GetComponent<Button>().onClick.AddListener(onOptionsPressed);
        help.GetComponent<Button>().onClick.AddListener(onHelpPressed);
        exit.GetComponent<Button>().onClick.AddListener(onExitPressed);
        share.GetComponent<Button>().onClick.AddListener(onSharePressed);
        rate.GetComponent<Button>().onClick.AddListener(onRatePressed);
	}

    private void onPlayPressed()
    {
        SceneManager.LoadScene("Save Manager");
    }

    private void onOptionsPressed()
    {
        SceneManager.LoadScene("Options");
    }

    private void onHelpPressed()
    {
        SceneManager.LoadScene("Help");
    }

    private void onExitPressed()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    
    private void onSharePressed()
    {
        Debug.Log("Share");
        //Share
    }

    private void onRatePressed()
    {
        Application.OpenURL("https://twitter.com/betehtin_art/status/837810270651187200");
    }

}
