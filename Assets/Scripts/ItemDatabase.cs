using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ItemDatabase : MonoBehaviour {
    
    private List<Item> database = new List<Item>();
    private JsonData itemsData;

    void Awake()
    {
        itemsData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); //get items from json file
        BuildItemDatabase(itemsData);
        Debug.Log(getItemByID(2));
    }

    //Returns instance of an item by its ID
    public Item getItemByID(int id)
    {
        foreach (Item item in database)
            if (item.id == id) return item;
        return null;
    }

    //create list of items to interact with
    void BuildItemDatabase(JsonData itemsData)
    {
        for (int i = 0; i < itemsData.Count; ++i)
        {
            database.Add(new Item(itemsData[i]));
        }
    }
}

//Base Item class
public class Item
{
    public int id;
    public string title;
    public bool stackable;
    public int maxStack;
    public string description;
    public string slug;
    public Sprite sprite;

    public Item(JsonData itemData)
    {
        this.id = (int)itemData["id"];
        this.title = itemData["title"].ToString();
        this.stackable = (bool)itemData["stackable"];
        this.maxStack = this.stackable ? (int)itemData["maxStack"] : 1;
        this.description = itemData["description"].ToString();
        this.slug = itemData["slug"].ToString();
        this.sprite = Resources.Load<Sprite>("Sprites/Items/" + this.slug);
        Debug.Log(id + " " + slug);
    }

}