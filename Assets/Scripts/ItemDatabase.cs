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
        BuildItemDatabase();
    }

    //Returns instance of an item by its ID
    public Item getItemByID(int id)
    {
        foreach (Item item in database)
            if (item.ID == id) return item;
        return null;
    }

    //create list of items to interact with
    void BuildItemDatabase()
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
    public int ID { get; set; }
    public string Title { get; set; }
    public bool Stackable { get; set; }
    public int MaxStack { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(JsonData itemData)
    {
        this.ID = (int)itemData["id"];
        this.Title = itemData["title"].ToString();
        this.Stackable = (bool)itemData["stackable"];
        this.MaxStack = this.Stackable ? (int)itemData["maxStack"] : 1;
        this.Slug = itemData["slug"].ToString();
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + this.Slug);
    }

}