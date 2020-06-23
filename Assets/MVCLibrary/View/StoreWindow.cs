using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreWindow : BaseWindow
{
    public Text dropText;
    public Text dropDescribe;
    public Text dropPrice;

    public StoreWindow()
    {
        resName = "UI/Window/StoreWindow";
        resident = true;
        selfType = WindowType.StoreWindow;
        scenesType = ScenesType.Login;
        StoreController.Instance.storeWindow = this;
    }

    protected override void Awake()
    {
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
        dropText = transform.Find("dropText").GetComponent<Text>();
        dropDescribe = transform.Find("dropDescribe").GetComponent<Text>();
        dropPrice = transform.Find("dropPrice").GetComponent<Text>();
        StoreController.Instance.ShowDrop(1);
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        foreach (var button in buttonList)
        {
            switch (button.name)
            {
                case "BuyButton":
                    button.onClick.AddListener(OnBuyButtonClick);
                    break;
            }
        }
    }
    
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (Input.GetKeyDown(KeyCode.C))
        {
            Close();
        }
    }


    private void OnBuyButtonClick()
    {
        Debug.Log("BuyButton 点击了！");
        StoreController.Instance.ShowDrop(2);
    }

}
