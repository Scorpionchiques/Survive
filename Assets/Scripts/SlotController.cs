using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotController : MonoBehaviour {

    public GameObject controls;
	public Button delete, load;

	void Start () {
        GetComponent<Button>().onClick.AddListener(slotControl);
        delete.GetComponent<Button>().onClick.AddListener(slotDelete);
        load.GetComponent<Button>().onClick.AddListener(slotLoad);
	}

    private void slotControl()
    {
        disableControls();

        if (PlayerPrefs.GetString(name) == "EMPTY")
        {
            if (newGameConfirmation()) {
                PlayerPrefs.SetString(name, "USED");
                SceneManager.LoadScene("Game");
            }
        }
        else
        {
            GetComponent<Button>().interactable = false;
            controls.SetActive(true);
        }
    }

    private void disableControls()
    {
        foreach (GameObject controlPanel in GameObject.FindGameObjectsWithTag("Controls"))
        {
            controlPanel.SetActive(false);
        }

        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Slot"))
        {
            slot.GetComponent<Button>().interactable = true;
        }
    }

    private void slotDelete()
    {
        if (deleteConfirmation())
        {
            PlayerPrefs.SetString(name, "EMPTY");
            controls.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
    }

    private void slotLoad()
    {
        if (loadConfirmation()) SceneManager.LoadScene("Game");
    }

    private bool newGameConfirmation()
    {
        return true;
    }

    private bool deleteConfirmation()
    {
        return true;
    }

    private bool loadConfirmation()
    {
        return true;
    }

}
