using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagWindow : BaseWindow
{
    public GameObject slotPrefab;
    public GameObject slotParent;
    public Text goldText;
    public Text capacity;
    public Button closeBtn;

    public BagWindow()
    {
        resName = "UI/Window/BagWindow";
        resident = true;
        selfType = WindowType.BagWindow;
        scenesType = ScenesType.Game;
        BagController.Instance.bagWindow = this;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        slotPrefab = Resources.Load<GameObject>("Slot");
        slotParent = transform.Find("ItemList/Area").gameObject;

        goldText = transform.Find("Money/Gold/Text").GetComponent<Text>();

        capacity = transform.Find("Capacity").GetComponent<Text>();

        closeBtn = transform.Find("Close").GetComponent<Button>();

        BagController.Instance.AddSlot(16);
        base.Awake();
    }

    protected override void OnAddListener()
    {
        base.OnAddListener();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        BagController.Instance.UpdateBagWindow();
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        closeBtn.onClick.AddListener(BagController.Instance.CloseBagWindow);
    }
}
