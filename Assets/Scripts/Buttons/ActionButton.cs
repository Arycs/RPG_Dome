using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private Button MyButton;

    public IUseable MyUseable
    {
        get;set;
    }

    private Stack<IUseable> useables = new Stack<IUseable>();
    public Stack<IUseable> MyUseables
    {
        get => useables;
        set
        {
            if (value.Count > 0)
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }
            useables = value;
        }
    }

    [SerializeField]
    private Text stackSize;
    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    [SerializeField]
    private Image icon;
    public Image MyIcon
    {
        get => icon;
        set => icon = value;
    }

    private int count;
    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public void OnClick()
    {
        if (HandScript.Instance.MyMoveable == null)
        {
            if (MyUseables != null )
            {
                if (MyUseable is Consumables)
                {
                    (MyUseables.Peek() as Item).MySlot.UseItem();
                }
                //if (MyUseable is Skill )z
                //{
                //    MyUseable.Use();
                //}
            }
        }
    }

    private void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        BagController.Instance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && MyUseables.Count >0)
        {
            if ((MyUseables.Peek() as Item).GetItemType() == item.GetItemType())
            {
                MyUseables = BagController.Instance.GetUseable(item as IUseable);
                count = MyUseables.Count;
                WindowManager.Instance.UpdateStackSize(this);

            }
        }

    }

    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            MyUseables = BagController.Instance.GetUseable(useable);
            if (BagController.Instance.FromSlot)
            {
                BagController.Instance.FromSlot.MyIcon.color = Color.white;
                BagController.Instance.FromSlot = null;
            }
        }
        else
        {
            MyUseables.Clear();
            MyUseable = useable;
        }
        count = MyUseables.Count;
        UpdateVisual(useable as IMoveable);
        //TODO 显示ToolTip
    }

    public void UpdateVisual(IMoveable moveable)
    {
        if (HandScript.Instance.MyMoveable != null)
        {
            HandScript.Instance.Drop();
        }

        MyIcon.sprite = moveable.MyIcon;
        MyIcon.color = Color.white;
        if (count >1)
        {
            WindowManager.Instance.UpdateStackSize(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.Instance.MyMoveable != null && HandScript.Instance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.Instance.MyMoveable as IUseable);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MyUseable is Item)
        {
            ToolTipsScript.Instance.ShowToolTips(MyUseable as Item);
        }
        //else if(MyUseable is Skill)
        //{
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipsScript.Instance.HideToolTips();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (MyUseables.Count != 0)
            {
                Debug.Log(transform.gameObject.name + "对应的物品是 : " + (MyUseables.Peek() as Item).MyItemName);
            }
            else
            {
                Debug.Log(transform.gameObject.name + "对应的物品是 :  空");
            }
        }
    }
}
