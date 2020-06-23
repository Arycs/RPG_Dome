using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverController : Singleton<QuestGiverController>
{
    public QuestGiverWindow questGiverWindow;

    private QuestGiver questGiver;

    private List<GameObject> quests = new List<GameObject>();

    private Quest selectedQuest;

    /// <summary>
    /// 显示任务
    /// </summary>
    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
        foreach (GameObject go in quests)
        {
            GameObject.Destroy(go);
        }
        quests.Clear();
        questGiverWindow.questArea.gameObject.SetActive(true);
        questGiverWindow.questDescription.gameObject.SetActive(false);

        foreach (Quest quest in questGiver.MyQuestInstances)
        {
            GameObject go = GameObject.Instantiate(questGiverWindow.questPrefab, questGiverWindow.questArea.transform);
            go.GetComponent<Text>().text = "[" + quest.MyLevel + "]" + quest.MyQuestName + "<color=#ffbb04><size=25>!</size></color>";
            go.GetComponent<QGQuestScript>().MyQuest = quest;
            go.GetComponent<QGQuestScript>().MyQuest.MyQuestGiver = questGiver;

            quests.Add(go);

            if (QuestController.Instance.HasQuest(quest) && quest.IsComplete)
            {
                go.GetComponent<Text>().text = quest.MyQuestName + "<color=#ffbb04><size=25> ? </size></color>";
            }

            if (QuestController.Instance.HasQuest(quest))
            {
                Color c = go.GetComponent<Text>().color;
                c.a = 0.5f;
                go.GetComponent<Text>().color = c;
                go.GetComponent<Text>().text = quest.MyQuestName + "<color=#c0c0c0ff><size=25>?</size></color>";
            }
            
        }

    }

    /// <summary>
    /// 显示任务详情
    /// </summary>
    /// <param name="quest"></param>
    public void ShowQuestInfo(Quest quest)
    {
        this.selectedQuest = quest;

        if (QuestController.Instance.HasQuest(quest) && quest.IsComplete)
        {
            questGiverWindow.acceptBtn.gameObject.SetActive(false);
            questGiverWindow.completeBtn.gameObject.SetActive(true);
        }
        else if (!QuestController.Instance.HasQuest(quest))
        {
            questGiverWindow.acceptBtn.gameObject.SetActive(true);
        }
        questGiverWindow.backBtn.gameObject.SetActive(true);

        questGiverWindow.questArea.gameObject.SetActive(false);
        questGiverWindow.questDescription.gameObject.SetActive(true);

        string description = quest.MyQuestDescription;
        string objectives = string.Empty;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ":" + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }
        foreach (Objective obj in quest.MyKillObjectives)
        {
            objectives += obj.MyType + ":" + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }
        questGiverWindow.questDescription.GetComponent<Text>().text = string.Format("<b>{0}</b>\n<size=25>{1}</size> \n 任务条件 \n <size=25>{2}</size>", quest.MyQuestName, quest.MyQuestDescription, objectives);
    }

    /// <summary>
    /// 返回任务列表
    /// </summary>
    public void Back()
    {
        questGiverWindow.backBtn.gameObject.SetActive(false);
        questGiverWindow.acceptBtn.gameObject.SetActive(false);

        ShowQuests(questGiver);
        questGiverWindow.completeBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// 接受任务
    /// </summary>
    public void Accept()
    {
        QuestController.Instance.AcceptQuest(selectedQuest);
        Back();
    }

    /// <summary>
    /// 打开NPC任务窗口
    /// </summary>
    /// <param name="questGiver"></param>
    public void OpenQuestGiverWindow(QuestGiver questGiver)
    {
        WindowManager.Instance.OpenWindow(WindowType.QuestGiverWindow);
        ShowQuests(questGiver);
        questGiverWindow.questDescription.gameObject.SetActive(false);
        questGiverWindow.acceptBtn.gameObject.SetActive(false);
        questGiverWindow.completeBtn.gameObject.SetActive(false);
        questGiverWindow.backBtn.gameObject.SetActive(false);
    }

    public void CloseQuestGiverWindow()
    {
        WindowManager.Instance.CloseWindow(WindowType.QuestGiverWindow);
    }

    public void CompleteQuest()
    {
        if (selectedQuest.IsComplete)
        {

            for (int i = 0; i < questGiver.MyQuests.Length; i++)
            {
                if (selectedQuest.MyQuestName == questGiver.MyQuestInstances[i].MyQuestName)
                {
                    questGiver.MyQuests[i] = null;
                    selectedQuest.MyQuestGiver.UpdateQuestStatue();
                }
            }
            //移除物品数量监听
            foreach (CollectObjective objective in selectedQuest.MyCollectObjectives)
            {
                BagController.Instance.itemCountChangedEvent -= new ItemCountChanged(objective.UpdateItemCount);
                objective.Complete();
            }
            foreach (KillObjective kill in selectedQuest.MyKillObjectives)
            {
                GameManager.Instance.KillConfirmedEvent -= new KillConfirmed(kill.UpdateKillCount);
            }

            //获得经验,奖励等

            foreach (var item in selectedQuest.RewardItems)
            {
                BagController.Instance.AddItem(item);
            }

            selectedQuest.MyQuestGiver.CreateQuestInstance();
            QuestController.Instance.RemoveQuest(selectedQuest.MyQuestScript);
            Back();
        }
    }
}
