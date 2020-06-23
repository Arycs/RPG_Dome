using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject , IMoveable,IDescribable
{
    [SerializeField]
    [Header("物品名字")]
    private string itemName;
    public string MyItemName
    {
        get => itemName;
    }

    [SerializeField]
    [Header("物品图标")]
    private Sprite icon;
    public Sprite MyIcon
    {
        get => icon;
    }

    [SerializeField]
    [Header("物品描述")]
    [TextArea(3,10)]
    private string description;
    public string MyDescription
    {
        get => description;
    }


    [SerializeField]
    [Header("物品可叠加上限")]
    private int stackSize;
    public int MyStackSize
    {
        get => stackSize;
    }
   

    [SerializeField]
    [Header("物品品质")]
    private Quality quality;
    public Quality MyQuality
    {
        get => quality;
    }

    [SerializeField]
    [Header("物品价格")]
    private int price;
    public int MyPrice
    {
        get => price;
    }

    /// <summary>
    /// 物品所在格子
    /// </summary>
    private SlotScript slot;
    public SlotScript MySlot
    {
        get => slot;
        set => slot = value;
    }

    private CharButton charButton;
    public CharButton MyCharButton
    {
        get => charButton;
        set
        {
            MySlot = null;
            charButton = value;
        }
    }

    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>\n 物品描述 : {2}", QualityColor.MyColors[MyQuality], MyItemName,MyDescription);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }

    public virtual string GetItemType()
    {
        return string.Empty;
    }
}


/// <summary>
/// 物品品级
/// </summary>
public enum Quality
{
    Common, Uncommon, Rare, Epic
}

public static class QualityColor
{
    public static Dictionary<Quality, string> MyColors { get; } = new Dictionary<Quality, string>() {
        { Quality.Common,"#ffffffff"},
        { Quality.Uncommon ,"#00ff00ff"},
        { Quality.Rare,"#0E6BECFF" },
        { Quality.Epic,"#A712dbff"}
    };
}