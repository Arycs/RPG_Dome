using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : Singleton<ItemModel>
{
    private Dictionary<string, Item> itemDic = new Dictionary<string, Item>();

    public void Add(Item item)
    {
        if (!itemDic.ContainsKey(item.MyItemName))
        {
            itemDic[item.MyItemName] = item;
        }
    }

    public Item GetItemByName(string itemName)
    {
        if (itemDic.ContainsKey(itemName))
        {
            return Object.Instantiate(itemDic[itemName]);
        }
        return null;
    }
}
