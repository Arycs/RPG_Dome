using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : Singleton<DialogController>
{
    public DialogWindow dialogWindow;

    public int curLine = 0;

    public DialogData dialogData;

    public QuestGiver npc;

    public void ShowDialogWindow(QuestGiver npc ,DialogData dialogData)
    {
        this.dialogData = dialogData;
        this.npc = npc;
        curLine = 0;

        dialogWindow.exitBtn.gameObject.SetActive(false);
        dialogWindow.questBtn.gameObject.SetActive(false);
        dialogWindow.shopBtn.gameObject.SetActive(false);

        dialogWindow.leftHead.SetActive(false);
        dialogWindow.rightHead.SetActive(false);

        dialogWindow.dialogContentText.gameObject.SetActive(true);
        WindowManager.Instance.OpenWindow(WindowType.DialogWindow);


        dialogWindow.rightHead.GetComponent<Image>().sprite = npc.MyHead;
        dialogWindow.dialogContentText.text = dialogData.contents[curLine].dialogText;
        if (dialogData.contents[curLine].npcDisplay)
        {
            dialogWindow.rightHead.SetActive(true);
        }
        else
        {
            dialogWindow.leftHead.SetActive(true);
        }
    }


    public void NextLine()
    {
        curLine++;
        if (curLine < dialogData.contents.Count)
        {
            dialogWindow.dialogContentText.text = dialogData.contents[curLine].dialogText;
            if (dialogData.contents[curLine].npcDisplay)
            {
                dialogWindow.rightHead.SetActive(true);
                dialogWindow.leftHead.SetActive(false);

            }
            else
            {
                dialogWindow.rightHead.SetActive(false);
                dialogWindow.leftHead.SetActive(true);
            }
        }
        else
        {
            if (dialogData.haveQuest)
            {
                dialogWindow.dialogContentText.gameObject.SetActive(false);
                dialogWindow.exitBtn.gameObject.SetActive(true);
                dialogWindow.questBtn.gameObject.SetActive(true);
            }
            else
            {
                CloseDialogWindow();
            }
        }
    }

    public void CloseDialogWindow()
    {
        WindowManager.Instance.CloseWindow(WindowType.DialogWindow);
    }

    public void OpenQuestGiverWindow()
    {
        QuestGiverController.Instance.OpenQuestGiverWindow(npc);
    }
}
