using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Singleton<CharacterController>
{
    public CharacterWindow characterWindow;


    public void OpenCharacterWindow()
    {
        WindowManager.Instance.OpenWindow(WindowType.CharacterWindow);
    }

    public void CloseCharacterWindow()
    {
        WindowManager.Instance.CloseWindow(WindowType.CharacterWindow);
    }

    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Head:
                characterWindow.charButtons[0].EquipArmor(armor);
                break;
            case ArmorType.Chest:
                characterWindow.charButtons[1].EquipArmor(armor);
                break;
            case ArmorType.Legs:
                characterWindow.charButtons[2].EquipArmor(armor);
                break;
            case ArmorType.Shoe:
                characterWindow.charButtons[3].EquipArmor(armor);
                break;
            case ArmorType.MainHand:
                characterWindow.charButtons[4].EquipArmor(armor);
                break;
            case ArmorType.OffHand:
                characterWindow.charButtons[5].EquipArmor(armor);
                break;
            default:
                break;
        }
    }

    public void UpdateCharacterAttribute()
    {
        //这里应该是角色的数值
        Player.Instance.MyPower = 10;
        Player.Instance.MyPhysical = 10;
        Player.Instance.MyIntellect = 10;
        Player.Instance.MyStamina = 10;
        Player.Instance.MyAgile = 10;

        foreach (var item in characterWindow.charButtons)
        {
            if (item.MyEquippedArmor != null)
            {
                Player.Instance.MyPower += item.MyEquippedArmor.MyPower;
                Player.Instance.MyPhysical += item.MyEquippedArmor.MyPhysical;
                Player.Instance.MyIntellect += item.MyEquippedArmor.MyIntellect;
                Player.Instance.MyStamina += item.MyEquippedArmor.MyStamina;
                Player.Instance.MyAgile  += item.MyEquippedArmor.MyAgile;
            }
        }
        characterWindow.damage.text ="伤害 ：" + (Player.Instance.MyPower * 10);
        characterWindow.power.text = "力量 ：" + Player.Instance.MyPower;


        Player.Instance.MyMaxHp = Player.Instance.MyPhysical * 10;
        characterWindow.health.text ="血量 ：" + (Player.Instance.MyHp > Player.Instance.MyPhysical * 10 ? Player.Instance.MyPhysical *10 : Player.Instance.MyHp) + "/" + (Player.Instance.MyPhysical * 10);
        characterWindow.physical.text ="体力 ：" + Player.Instance.MyPhysical;

        Player.Instance.MyMaxMana = Player.Instance.MyIntellect * 10;
        characterWindow.mana.text ="蓝量 ：" + (Player.Instance.MyMana > Player.Instance.MyIntellect * 10 ? Player.Instance.MyIntellect * 10 : Player.Instance.MyMana) + "/" + (Player.Instance.MyIntellect * 10);
        characterWindow.intelligence.text ="智力 ：" + Player.Instance.MyIntellect;

        characterWindow.armor.text = "护甲 ：" + (Player.Instance.MyStamina * 10);
        characterWindow.stamina.text = "耐力 ：" + Player.Instance.MyStamina;

        characterWindow.critical.text = "回避 ：" + Player.Instance.MyAgile  + "%";
        characterWindow.agile.text = "敏捷 ：" +Player.Instance.MyAgile .ToString();
    }
}
