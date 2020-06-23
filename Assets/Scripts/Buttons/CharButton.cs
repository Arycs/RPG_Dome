using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor equippedArmor;
    public Armor MyEquippedArmor
    {
        get => equippedArmor;
        set => equippedArmor = value;
    }

    [SerializeField]
    private Image icon;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (HandScript.Instance.MyMoveable == null && MyEquippedArmor !=null)
            {
                DequipArmor();
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        armor.Remove();
        if (MyEquippedArmor != null)
        {
            if (MyEquippedArmor != armor)
            {
                //替换装备
                armor.MySlot.AddItem(MyEquippedArmor);
            }
        }
        else
        {

        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;
        MyEquippedArmor = armor;
        MyEquippedArmor.MyCharButton = this;
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        MyEquippedArmor.MyCharButton = null;
        BagController.Instance.AddItem(MyEquippedArmor);
        MyEquippedArmor = null;
        CharacterController.Instance.UpdateCharacterAttribute();

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MyEquippedArmor!=null)
        {
            ToolTipsScript.Instance.ShowToolTips(MyEquippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipsScript.Instance.HideToolTips();
    }
}
