using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowManager : MonoSingleton<WindowManager>
{
    Dictionary<WindowType, BaseWindow> windowDic = new Dictionary<WindowType, BaseWindow>();
    //构造函数初始化，添加都有那些界面
    public WindowManager()
    {
        windowDic.Add(WindowType.BagWindow, new BagWindow());
        windowDic.Add(WindowType.MainWindow, new MainWindow());
        windowDic.Add(WindowType.QuestWindow, new QuestWindow());
        windowDic.Add(WindowType.QuestGiverWindow, new QuestGiverWindow());
        windowDic.Add(WindowType.CharacterWindow, new CharacterWindow());
        windowDic.Add(WindowType.DialogWindow, new DialogWindow());


    }

    public void Update()
    {
        foreach (var window in windowDic.Values)
        {
            if (window.IsVisible())
            {
                window.Update(Time.deltaTime);
            }
        }
    }

    //打开窗口
    public BaseWindow OpenWindow(WindowType type)
    {
        BaseWindow window;
        if (windowDic.TryGetValue(type, out window))
        {
            window.Open();
            return window;
        }
        else
        {
            Debug.LogError($"Open Error:{type}");
            return null;
        }
    }

    //关闭窗口 
    public BaseWindow CloseWindow(WindowType type)
    {
        BaseWindow window;
        if (windowDic.TryGetValue(type, out window))
        {
            window.Close();
            return window;
        }
        else
        {
            Debug.LogError($"Close Error:{type}");
            return null;
        }
    }

    //预加载
    public void PreLoadWindow(ScenesType type)
    {
        foreach (var item in windowDic.Values)
        {
            if (item.GetScnensType() == type)
            {
                item.PreLoad();
            }
        }
    }

    //隐藏掉某个类型的所有窗口
    public void HideAllWindow(ScenesType type , bool isDestory = false)
    {
        foreach (var item in windowDic.Values)
        {
            if (item.GetScnensType() == type)
            {
                item.Close(isDestory);
            }
        }
    }


    /// <summary>
    /// 更新快捷栏上的物品数量
    /// </summary>
    /// <param name="clickable"></param>
    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }


    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
