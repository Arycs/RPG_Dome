using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private WindowType windowtype;

    [SerializeField]
    protected DialogData[] dialogDatas;

    public int dialogNum;

    [SerializeField]
    private Sprite head;
    public Sprite MyHead
    {
        get => head;
    }
}
