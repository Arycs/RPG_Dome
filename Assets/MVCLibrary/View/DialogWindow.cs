using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : BaseWindow
{
    public GameObject leftHead;
    public GameObject rightHead;
    public Text dialogContentText;
    public Button questBtn;
    public Button shopBtn;
    public Button exitBtn;

    public DialogWindow()
    {
        resName = "UI/Window/DialogWindow";
        resident = true;
        selfType = WindowType.DialogWindow;
        scenesType = ScenesType.Game;
        DialogController.Instance.dialogWindow = this;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (IsVisible())
        {
            if (Input.GetMouseButtonDown(0))
            {
                DialogController.Instance.NextLine();
            }  
        }
    }

    protected override void Awake()
    {
        leftHead = transform.Find("LeftHead").gameObject;
        rightHead = transform.Find("RightHead").gameObject;
        dialogContentText = transform.Find("DialogBg/DialogContent").GetComponent<Text>();
        questBtn = transform.Find("DialogBg/Buttons/Quest").GetComponent<Button>();
        shopBtn = transform.Find("DialogBg/Buttons/Shop").GetComponent<Button>();
        exitBtn = transform.Find("DialogBg/Buttons/Exit").GetComponent<Button>();
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
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        exitBtn.onClick.AddListener(DialogController.Instance.CloseDialogWindow);
        questBtn.onClick.AddListener(DialogController.Instance.OpenQuestGiverWindow);
        base.RegisterUIEvent();
    }
}
