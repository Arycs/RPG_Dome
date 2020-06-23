using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BagController : Singleton<BagController>
{
    public event ItemCountChanged itemCountChangedEvent;

    public BagWindow bagWindow;

    private List<SlotScript> slots = new List<SlotScript>();
    public List<SlotScript> MySlots
    {
        get => slots;
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach (SlotScript slot in MySlots)
            {
                if (slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public int MySlotCount
    {
        get
        {
            return MySlots.Count;
        }
    }

    private SlotScript fromSlot;
    public SlotScript FromSlot
    {
        get => fromSlot;
        set
        {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    /// <summary>
    /// 添加背包格子
    /// </summary>
    public void AddSlot(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Object.Instantiate(bagWindow.slotPrefab,bagWindow.slotParent.transform).GetComponent<SlotScript>();
            slot.MyIndex = i;
            MySlots.Add(slot);
        }
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }
        return PlaceInEmpty(item);
    }

    /// <summary>
    /// 判断是否有空位添加物品
    /// </summary>
    private bool PlaceInEmpty(Item item)
    {
        foreach (SlotScript slot in MySlots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断是否有可以叠加的物品
    /// </summary>
    private bool PlaceInStack(Item item)
    {
        foreach (SlotScript slotScript in MySlots)
        {
            if (slotScript.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取可使用的物品
    /// </summary>
    public Stack<IUseable> GetUseable(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();
        Item temp = type as Item;
        foreach (SlotScript slot in MySlots)
        {
            if (!slot.IsEmpty && slot.MyItem.GetItemType() == temp.GetItemType())
            {
                foreach (Item item in slot.MyItems)
                {
                    useables.Push(item as IUseable);
                }
            }
        }
        return useables;
    }

    /// <summary>
    /// 得到物品数量,用于任务判断是否完成
    /// </summary>
    public int GetItemCount(string name)
    {
        int itemCount = 0;
        foreach (SlotScript slot in MySlots)
        {
            if (!slot.IsEmpty && slot.MyItem.MyItemName == name)
            {
                itemCount += slot.MyItems.Count;
            }
        }
        return itemCount;
    }

    /// <summary>
    /// 得到物品和数量,用于提交任务
    /// </summary>
    public Stack<Item> GetItems(string name,int count)
    {
        Stack<Item> items = new Stack<Item>();
        foreach (SlotScript slot in MySlots)
        {
            if (!slot.IsEmpty && slot.MyItem.MyItemName == name)
            {
                foreach (Item item in slot.MyItems)
                {
                    items.Push(item);
                    if (items.Count== count)
                    {
                        return items;
                    }
                }
            }
        }
        return items;
    }

    public void OpenBagWindow()
    {
        WindowManager.Instance.OpenWindow(WindowType.BagWindow);
    }

    public void CloseBagWindow()
    {
        WindowManager.Instance.CloseWindow(WindowType.BagWindow);
    }

    public void UpdateBagWindow()
    {
        bagWindow.capacity.text = (MySlotCount - MyEmptySlotCount).ToString() + " / " + MySlotCount.ToString();
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            UpdateBagWindow();
            itemCountChangedEvent.Invoke(item);
        }
    }


    #region 用于存储游戏
    /// <summary>
    /// 保存物品的具体格子索引
    /// </summary>
    public void PlaceInSpecific(Item item, int slotIndex)
    {
        MySlots[slotIndex].AddItem(item);
    }

    /// <summary>
    /// 获取全部物品
    /// </summary>
    public List<SlotScript> GetAllItems()
    {
        List<SlotScript> slots = new List<SlotScript>();
        foreach (SlotScript slot in MySlots)
        {
            if (!slot.IsEmpty)
            {
                slots.Add(slot);
            }
        }
        return slots;
    }
    #endregion
}
