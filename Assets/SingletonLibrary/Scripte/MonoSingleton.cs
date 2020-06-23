using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("MonoSingleton");
                if (go == null)
                {
                    go = new GameObject("MonoSingleton");
                    go.AddComponent<T>();
                    instance = go.GetComponent<T>();
                }
                else
                {
                    instance = go.GetComponent<T>();
                    if (instance == null)
                    {
                        go.AddComponent<T>();
                        instance = go.GetComponent<T>();
                    }
                }
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
}

