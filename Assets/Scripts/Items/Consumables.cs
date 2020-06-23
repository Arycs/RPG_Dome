using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumablesType
{
    HealthPotion,ManaPotion
}

[CreateAssetMenu(fileName ="消耗品",menuName ="物品/消耗品")]
public class Consumables : Item,IUseable
{

    [SerializeField]
    [Header("消耗品类型")]
    public ConsumablesType consumablesType;

    [SerializeField]
    [Header("回复血量")]
    private int health;

    [SerializeField]
    [Header("回复蓝量")]
    private int mana;

    public bool Use()
    {
        //TODO 
        //判断血量或者蓝量， Player 加血量，
        if (health > 0 && Player.Instance.MyHp < Player.Instance.MyMaxHp)
        {
            Debug.Log("使用了HP药剂，恢复了血量");
            Player.Instance.MyHp += health;
            if (Player.Instance.MyHp > Player.Instance.MyMaxHp)
            {
                Player.Instance.MyHp = Player.Instance.MyMaxHp;
            }
            return true;
        }
        if (mana > 0 && Player.Instance.MyMana < Player.Instance.MyMaxMana)
        {
            Debug.Log("使用了MP药剂，恢复了蓝量");
            Player.Instance.MyMana += mana;
            if (Player.Instance.MyMana > Player.Instance.MyMaxMana)
            {
                Player.Instance.MyMana = Player.Instance.MyMaxMana;
            }
            return true;
        }
        return false;
    }

    public override string GetDescription()
    {
        string stats = string.Empty;
        if (health > 0)
        {
            stats += string.Format("\n <color=#00ff00ff> 使用 ： 回复 {0} 血量 </color>", health);
        }
        if (mana > 0)
        {
            stats += string.Format("\n <color=#00ff00ff> 使用 ： 回复 {0} 蓝量 </color>", mana);
        }

        return base.GetDescription() + stats;
    }

    public override string GetItemType()
    {
        return consumablesType.ToString();
    }
}
