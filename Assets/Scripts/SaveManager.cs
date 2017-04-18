using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour {

    public Button back;

	void Start () {
        back.GetComponent<Button>().onClick.AddListener(returnToMenu);
    }
	
    private void returnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
}
