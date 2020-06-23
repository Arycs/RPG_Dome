using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardButton : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    public Image MyIcon
    {
        get => icon;
        set => icon = value;
    }

    [SerializeField]
    private Text count;
    public Text MyCount
    {
        get => count;
        set => count = value;
    }
}
