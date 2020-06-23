using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="对话")]
public class DialogData : ScriptableObject
{
    public List<DialogContent> contents;
    public bool haveQuest;
}

[System.Serializable]
public class DialogContent
{
    [TextArea(3,10)]
    public string dialogText;
    public bool npcDisplay;
}