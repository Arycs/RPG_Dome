using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;
    public static HandScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }
    }

    public IMoveable MyMoveable
    {
        get;set;
    }
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButtonDown(0) && ! EventSystem.current.IsPointerOverGameObject() && MyMoveable != null)
        {
            DeleteItem();
        }
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        BagController.Instance.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item)
        {
            Item item = (Item)MyMoveable;
            if (item.MySlot != null)
            {
                item.MySlot.Clear();
            }
        }
        Drop();
    }

    public void TakeMoveable(IMoveable moveable)
    {
        MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }
}
