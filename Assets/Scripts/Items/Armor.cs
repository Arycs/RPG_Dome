using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType
{
    Head, Chest, Legs, Shoe, MainHand, OffHand
}

[CreateAssetMenu(fileName = "装备", menuName = "物品/装备")]
public class Armor : Item
{
    [SerializeField]
    [Header("装备类型")]
    private ArmorType armorType;
    internal ArmorType MyArmorType
    {
        get => armorType;
    }

    [SerializeField]
    [Header("力量")]
    private int power;
    public int MyPower
    {
        get => power;
    }

    [SerializeField]
    [Header("体力")]
    private int physical;
    public int MyPhysical
    {
        get => physical;
    }


    [SerializeField]
    [Header("智力")]
    private int intellect;
    public int MyIntellect
    {
        get => intellect;
    }

    [SerializeField]
    [Header("耐力")]
    private int stamina;
    public int MyStamina
    {
        get => stamina;
    }

    [SerializeField]
    [Header("敏捷")]
    private int agile;
    public int MyAgile
    {
        get => agile;
    }

    public override string GetDescription()
    {
        string stats = string.Empty;
        if (intellect > 0)
        {
            stats += string.Format("\n + {0} 智力", intellect);
        }
        if (power > 0)
        {
            stats += string.Format("\n + {0} 力量", power);
        }
        if (stamina > 0)
        {
            stats += string.Format("\n + {0} 体力", stamina);
        }
        return base.GetDescription() + stats;
    }

    public override string GetItemType()
    {
        return armorType.ToString();
    }

    /// <summary>
    /// 装备
    /// </summary>
    public void Equip()
    {
        CharacterController.Instance.EquipArmor(this);
        CharacterController.Instance.UpdateCharacterAttribute();
    }
}
