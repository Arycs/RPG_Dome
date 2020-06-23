using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCTest : MonoBehaviour
{
   
    public Item[] items;

    private void Awake()
    {
        foreach (Item item in items)
        {
            ItemModel.Instance.Add(item);
        }
        WindowManager.Instance.PreLoadWindow(ScenesType.Game);
        WindowManager.Instance.OpenWindow(WindowType.MainWindow);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BagController.Instance.OpenBagWindow();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("天国王朝盾"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("天国盾"));

            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("正阳袍"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("正阳冠"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("正阳鞋"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("正阳腿"));

            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("穷蝉盔"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("穷蝉铠"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("穷蝉鞋"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("穷蝉腿"));

            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("长鲸"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("天诛"));

            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("血量药水"));
            BagController.Instance.AddItem(ItemModel.Instance.GetItemByName("魔法药水"));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            BagController.Instance.AddSlot(16);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.Instance.MyHp -= 10;
            if (Player.Instance.MyTarget != null)
            {
                Debug.Log(Player.Instance.MyTarget.name);
                Player.Instance.MyTarget.GetComponent<Enemy>().TakeDamage(10,Player.Instance.transform);
            }
        }
    }

    
}
