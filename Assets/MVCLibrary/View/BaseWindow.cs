﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseWindow
{
    protected Transform transform; // 窗体
    protected string resName;//资源名称
    protected bool resident;//是否常驻
    protected bool visible = false;//是否可见
    protected WindowType selfType;//窗体类型
    protected ScenesType scenesType;//场景类型

    //UI控件  按钮
    protected Button[] buttonList;    //按钮列表

    //需要给子类提供的接口
    //初始化
    protected virtual void Awake()
    {
        //true 表示隐藏的物体也会查找
        buttonList = transform.GetComponentsInChildren<Button>(true);
        RegisterUIEvent();
    }

    /// <summary>
    /// UI事件的注册
    /// </summary>
    protected virtual void RegisterUIEvent()
    {

    }

    //添加监听游戏事件
    protected virtual void OnAddListener()
    {

    }

    //移除游戏事件
    protected virtual void OnRemoveListener()
    {

    }

    //每次打开
    protected virtual void OnEnable()
    {

    }

    //每次关闭
    protected virtual void OnDisable()
    {

    }

    //每帧更新
    public virtual void Update(float deltaTime)
    {

    }


    // -------------------WindowManager
    public void Open()
    {
        if (transform == null)
        {
            if (Create())
            {
                Awake();
            }
        }
        if (transform.gameObject.activeSelf == false)
        {
            UIRoot.SetParent(transform, true, selfType == WindowType.TipsWindow);
            transform.gameObject.SetActive(true);
            visible = true;
            OnEnable();  //调用激活时候触发的事件
            OnAddListener(); // 添加事件
        }

    }

    public void Close(bool isDestory = false)
    {
        if (transform.gameObject.activeSelf == true)
        {
            OnRemoveListener(); //移除事件
            OnDisable(); //调用隐藏时候触发的事件
            if (isDestory == false)
            {
                if (resident)
                {
                    transform.gameObject.SetActive(false);
                    UIRoot.SetParent(transform, false, false);
                }
                else
                {
                    GameObject.Destroy(transform.gameObject);
                    transform = null;
                }
            }
            else
            {
                GameObject.Destroy(transform.gameObject);
                transform = null;
            }
            
        } 
        //不可见状态
        visible = false;
    }

    public void PreLoad()
    {
        if (transform ==null)
        {
            if (Create())
            {
                Awake();
            }
        }
    }

    /// <summary>
    /// 获取场景类型
    /// </summary>
    /// <returns></returns>
    public ScenesType GetScnensType()
    {
        return scenesType;
    }

    /// <summary>
    /// 获取窗体类型
    /// </summary>
    /// <returns></returns>
    public WindowType GetWindowType()
    {
        return selfType;
    }

    /// <summary>
    /// 获取根节点
    /// </summary>
    /// <returns></returns>
    public Transform GetRoot()
    {
        return transform;
    }

    /// <summary>
    /// 是否可见
    /// </summary>
    /// <returns></returns>
    public bool IsVisible()
    {
        return visible;
    }

    /// <summary>
    /// 是否常驻
    /// </summary>
    /// <returns></returns>
    public bool IsResident()
    {
        return resident;
    }

    //----内部----
    private bool Create()
    {
        if (string.IsNullOrEmpty(resName))
        {
            return false;
        }

        if (transform == null)
        {
            var obj = Resources.Load<GameObject>(resName);
            if (obj == null)
            {
                Debug.LogError($"未找到UI预制件{selfType}");
                return false;
            }
            transform =  GameObject.Instantiate(obj).transform;

            transform.gameObject.SetActive(false);

            UIRoot.SetParent(transform, false, selfType == WindowType.TipsWindow);

            return true;
        }
        return true;
    }
}

/// <summary>
/// 窗体类型
/// </summary>
public enum WindowType
{
    LoginWindow,
    StoreWindow,
    TipsWindow,
    BagWindow,
    MainWindow,
    QuestWindow,
    QuestGiverWindow,
    CharacterWindow,
    DialogWindow
}

/// <summary>
/// 场景类型,目的根据场景类型进行预加载
/// </summary>
public enum ScenesType
{
    None,
    Login,
    Battle,
    Game
}