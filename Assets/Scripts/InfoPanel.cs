using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Create object to easily access Info Panel paremeters
public class InfoPanel
{
    public GameObject panel;
    private Text title; // item name
    private Text description; // item description
    private Text parameters; // item parameters
    private Image itemImage; // item sprite
    private Button drop; // drop button
    private Text dropText; // text on drop button
    private Button use; // use button
    private Text useText; // text on use button

    /* 
     * Get panel object and make all the references to its children
     * so they can easily be refered to
     */
    public InfoPanel(GameObject panel)
    {
        this.panel = panel;
        this.title = panel.transform.Find("Title/Text").GetComponent<Text>();
        this.description = panel.transform.Find("Description").GetComponent<Text>();
        this.parameters = panel.transform.Find("Parameters").GetComponent<Text>();
        this.itemImage = panel.transform.Find("ItemImage").GetComponent<Image>();
        this.drop = panel.transform.Find("ButtonPanel/Drop").GetComponent<Button>();
        this.dropText = panel.transform.Find("ButtonPanel/Drop/Text").GetComponent<Text>();
        this.use = panel.transform.Find("ButtonPanel/Use").GetComponent<Button>();
        this.useText = panel.transform.Find("ButtonPanel/Use/Text").GetComponent<Text>();

        /*
        Debug.Log(Title.text);
        Debug.Log(Description.text);
        Debug.Log(Parameters.text);
        Debug.Log(ItemImage);
        Debug.Log(Drop);
        Debug.Log(DropText.text);
        Debug.Log(Use);
        Debug.Log(UseText.text);
        */
    }

    // Set info panel values according to selected item
    public void setValues(Item item)
    {
        title.text = item.title;
        description.text = item.description;
        parameters.text = setParametersText(item);
        itemImage.sprite = item.sprite;

        /*
         Implement button changes depending on item type
         */

    }

    //Implement item classes and parameters. Then rewrite this function
    private string setParametersText(Item item)
    {
        return "not implemented";
    }

}