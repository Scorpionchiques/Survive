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
            PlayerPrefs.SetString(name, "USED");
            newGameConfirmation();
        }
        else
        {
            GetComponent<Button>().interactable = false;
            controls.SetActive(true);
        }
    }

    private void disableControls()
    {
        var otherSlotControls = GameObject.FindGameObjectsWithTag("Controls");
        foreach (GameObject controlPanel in otherSlotControls)
        {
            controlPanel.SetActive(false);
        }
        var otherSlots = GameObject.FindGameObjectsWithTag("Slot");
        foreach (GameObject slot in otherSlots)
        {
            slot.GetComponent<Button>().interactable = true;
        }
    }

    private void slotDelete()
    {
        if (deleteConfirm())
        {
            PlayerPrefs.SetString(name, "EMPTY");
            controls.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
    }

    private void slotLoad()
    {
        if (loadConfirm()) SceneManager.LoadScene("Game");
    }

}
