using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IClickable,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 物品栈，用来堆叠数量
    /// </summary>
    private ObservableStack<Item> items = new ObservableStack<Item>();
    public ObservableStack<Item> MyItems
    {
        get => items;
    }
    
    [SerializeField]
    [Header("当前格子上的物品图标")]
    private Image icon;
    public Image MyIcon
    {
        get => icon;
        set => icon = value;
    }
    
    [SerializeField]
    [Header("当前格子上物品的数量")]
    private Text stackSize;
    public Text MyStackText 
    {
        get => stackSize;
    }
    
    /// <summary>
    /// 当前格子是否为空
    /// </summary>
    private bool isEmpty;
    public bool IsEmpty
    {
        get {
            return MyItems.Count == 0;
        }
    }

    /// <summary>
    /// 当前格子是否满了
    /// </summary>
    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 当前格子的物品
    /// </summary>
    public Item MyItem
    {
        get
        {
            if (!isEmpty)
            {
                return MyItems.Peek();
            }
            return null;
        }
    }
    
    /// <summary>
    /// 格子编号
    /// </summary>
    private int index;
    public int MyIndex
    {
        get => index;
        set => index = value;
    }

    public int MyCount
    {
        get
        {
            return MyItems.Count;
        }
    }


    private void Awake()
    {
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    /// <summary>
    /// 给当前格子添加物品
    /// </summary>
    public bool AddItem(Item item)
    {
        MyItems.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;
        return true;
    }

    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetItemType()==MyItem.GetItemType())
        {
            int count = newItems.Count;
            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }
                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            if ((MyItem as Consumables).Use())
            {
                RemoveItem(MyItem);
            }
        }
        else if (MyItem is Armor)
        {
            (MyItem as Armor).Equip();
        }
    }

    /// <summary>
    /// 叠加物品
    /// </summary>
    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.MyItemName == MyItem.MyItemName && MyItems.Count < MyItem.MyStackSize) 
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 将物品放回原处
    /// </summary>
    public bool PutItemBack()
    {
        if (BagController.Instance.FromSlot == this)
        {
            BagController.Instance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 交换物品
    /// </summary>
    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetItemType() != MyItem.GetItemType() || from.MyCount + MyCount > MyItem.MyStackSize)
        {
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

            from.MyItems.Clear();
            from.AddItems(MyItems);

            MyItems.Clear();
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }

    /// <summary>
    /// 合并物品
    /// </summary>
    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetItemType() == MyItem.GetItemType() && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;
            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 移除物品，调用事件更新快捷栏上的UI
    /// </summary>
    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            BagController.Instance.OnItemCountChanged(MyItems.Pop());
        }
    }

    /// <summary>
    /// 更新格子上的内容
    /// </summary>
    private void UpdateSlot()
    {
        WindowManager.Instance.UpdateStackSize(this);
    }

    public void Clear()
    {
        int initCount = MyItems.Count;
        if (initCount > 0)
        {
            for (int i = 0; i < initCount; i++)
            {
                BagController.Instance.OnItemCountChanged(MyItems.Pop());
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        /* 鼠标左键操作
         * 1.当前手中有物品
         *      点击有物品的格子 - 进行交换
         *      点击无物品的格子 - 进行移动
         * 2.当前手中无物品
         *      点击有物品的格子 - 拾起物品
         *      
         * 鼠标右键操作
         * 1.当前手中无物品
         *      消耗品 - 使用， 装备 - 装备
         * 2.当前手中有物品
         *      不进行操作
        */
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //手中没有东西，并且点击格子不为空 ， 拾起物品
            if (BagController.Instance.FromSlot == null && !IsEmpty)
            {
                if (HandScript.Instance.MyMoveable == null)
                {
                    Debug.Log(MyItem.MyItemName);
                    HandScript.Instance.TakeMoveable(MyItem as IMoveable);
                    BagController.Instance.FromSlot = this;
                }
            }
            //手中有东西
            else if (BagController.Instance.FromSlot == null && IsEmpty)
            {
                HandScript.Instance.Drop();
            }
            else if (BagController.Instance.FromSlot != null)
            {
                if (PutItemBack() || MergeItems(BagController.Instance.FromSlot) || SwapItems(BagController.Instance.FromSlot) || AddItems(BagController.Instance.FromSlot.MyItems))
                {
                    HandScript.Instance.Drop();
                    BagController.Instance.FromSlot = null;
                }
            }
            #region Bug
                //if (BagController.Instance.FromSlot == null && !IsEmpty)
                //{
                //    if (HandScript.Instance.MyMoveable == null)
                //    {
                //        HandScript.Instance.TakeMoveable(MyItem as IMoveable);
                //        BagController.Instance.FromSlot = this;
                //    }
                //}
                //else if (BagController.Instance.FromSlot == null && IsEmpty)
                //{
                //    HandScript.Instance.Drop();
                //}
                ////手中有东西
                //else if (BagController.Instance.FromSlot != null)
                //{
                //    if (PutItemBack() || MergeItems(BagController.Instance.FromSlot) || SwapItems(BagController.Instance.FromSlot)|| AddItems(BagController.Instance.FromSlot.MyItems))
                //    {
                //        HandScript.Instance.Drop();
                //        BagController.Instance.FromSlot = null;
                //    }
                //}
                #endregion
        }
        if (eventData.button == PointerEventData.InputButton.Right && !IsEmpty)
        {
            if (HandScript.Instance.MyMoveable == null)
            {
                UseItem();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO 显示ToolTips
        if (MyItems.Count > 0)
        {
            ToolTipsScript.Instance.ShowToolTips(MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO 隐藏ToolTips
        ToolTipsScript.Instance.HideToolTips();
    }

}
