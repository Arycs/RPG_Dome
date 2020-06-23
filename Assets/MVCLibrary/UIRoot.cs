using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot
{
    static Transform transform;
    //回收的窗体，隐藏
    static Transform recyclePool;
    //前台显示的窗体
    static Transform workstation;
    //提示类型的窗体
    static Transform noticestation;

    static bool isInit = false;

    public static void Init()
    {
        
        if (transform == null)
        {
            transform = GameObject.FindWithTag("UIRoot").transform;
            if (transform == null)
            {
                var obj = Resources.Load<GameObject>("UI/UIRoot");
                transform = GameObject.Instantiate(obj).transform;
            }
            
        }

        if (recyclePool == null)
        {
            recyclePool = transform.Find("recyclePool");
        }

        if (workstation == null)
        {
            workstation = transform.Find("workstation");
        }

        if (noticestation == null)
        {
            noticestation = transform.Find("noticestation");
        }
        isInit = true;
    }

    public static void SetParent(Transform window, bool isOpen ,bool isTipsWindow = false)
    {
        if (isInit == false)
        {
            Init();
        }

        if (isOpen == true)
        {
            if (isTipsWindow)
            {
                window.SetParent(noticestation, false);
            }
            else
            {
                window.SetParent(workstation, false);
            }
        }
        else
        {
            window.SetParent(recyclePool, false);
        }
    }
}
