using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest
    {
        get;set;
    }

    public bool markedComplete
    {
        get;set;
    }

    public void Select()
    {
        GetComponent<Text>().color = Color.red;
        QuestController.Instance.ShowQuestDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;
    }

    public void IsComplete()
    {

    }
}
