using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : Singleton<QuestController>
{
    public QuestWindow questWindow;

    public List<QuestScript> questScripts = new List<QuestScript>();

    private List<Quest> quests = new List<Quest>();
    public List<Quest> MyQuests
    {
        get => quests;
        set => quests = value;
    }

    public void OpenQuestWindow()
    {
        WindowManager.Instance.OpenWindow(WindowType.QuestWindow);
    }

    public void CloseQuestWindow()
    {
        WindowManager.Instance.CloseWindow(WindowType.QuestWindow);
    }

    private Quest selected;

    /// <summary>
    /// 接受任务
    /// </summary>
    public void AcceptQuest(Quest quest)
    {
        foreach (CollectObjective item in quest.MyCollectObjectives)
        {
            BagController.Instance.itemCountChangedEvent += new ItemCountChanged(item.UpdateItemCount);
            item.UpdateItemCount();
        }
        foreach (KillObjective kill in quest.MyKillObjectives)
        {
            //未检测杀敌数量事件添加监听
            GameManager.Instance.KillConfirmedEvent += new KillConfirmed(kill
                .UpdateKillCount);

        }

        quests.Add(quest);
        GameObject go = GameObject.Instantiate(questWindow.questPrefab, questWindow.questParent.transform);
        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        questScripts.Add(qs);

        go.GetComponent<Text>().text = quest.MyQuestName;
        CheckCompletion();
    }

    public void UpdateSelected()
    {
        ShowQuestDescription(selected);
    }

    public void CheckCompletion()
    {
        foreach (QuestScript qs in questScripts)
        {
            qs.MyQuest.MyQuestGiver.UpdateQuestStatue();
            qs.IsComplete();
        }
    }

    /// <summary>
    /// 显示任务详情
    /// </summary>
    public void ShowQuestDescription(Quest quest)
    {
        if (quest !=null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }

            string objectives = string.Empty;
            selected = quest;

           

            string title = quest.MyQuestName;
            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objectives += obj.MyType + ":" + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            foreach (Objective obj in quest.MyKillObjectives)
            {
                objectives += obj.MyType + ":" + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            for (int i = 0; i < questWindow.items.Length; i++)
            {
                var reward = questWindow.items[i].gameObject.transform.Find("Icon");
                reward.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
            for (int i = 0; i < quest.RewardItems.Length; i++)
            {
                var reward = questWindow.items[i].gameObject.transform.Find("Icon");

                reward.GetComponent<Image>().sprite = quest.RewardItems[i].MyIcon;
                reward.GetComponent<Image>().color = new Color(255,255,255,255);
            }

            questWindow.questTitle.text = title;
            questWindow.exp.text = "奖励金币： " + quest.MyExp.ToString();
            questWindow.money.text = "奖励经验： " + quest.MyMoney.ToString();
            questWindow.questDescription.text = string.Format("<size=20>{0}</size> \n <size=30>任务条件</size> \n <size=25>{1}</size>", quest.MyQuestDescription, objectives);
        }
    }

    public void AbandonQuest()
    {
        if (selected == null)
        {
            return ;
        }
        foreach (CollectObjective o in selected.MyCollectObjectives)
        {
            BagController.Instance.itemCountChangedEvent -= new ItemCountChanged(o.UpdateItemCount);
        }

        foreach (KillObjective o in selected.MyKillObjectives)
        {
            GameManager.Instance.KillConfirmedEvent -= new KillConfirmed(o.UpdateKillCount);
        }

        RemoveQuest(selected.MyQuestScript);
    }

    public void RemoveQuest(QuestScript qs)
    {
        questScripts.Remove(qs);
        GameObject.Destroy(qs.gameObject);
        quests.Remove(qs.MyQuest);
        questWindow.questDescription.text = string.Empty;
        selected = null;
        qs.MyQuest.MyQuestGiver.UpdateQuestStatue();
        qs = null;
        for (int i = 0; i < questWindow.items.Length; i++)
        {
            var reward = questWindow.items[i].gameObject.transform.Find("Icon");
            reward.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public bool HasQuest(Quest quest)
    {
        return quests.Exists(x => x.MyQuestName == quest.MyQuestName);
    }
}
