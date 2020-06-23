using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest :ScriptableObject
{
    [SerializeField]
    private string questName;
    public string MyQuestName
    {
        get => questName;
        set => questName = value;
    }

    [TextArea(3,10)]
    [SerializeField]
    private string questDescription;
    public string MyQuestDescription
    {
        get => questDescription;
        set => questDescription = value;
    }

    [SerializeField]
    private CollectObjective[] collectObjectives;
    public CollectObjective[] MyCollectObjectives
    {
        get => collectObjectives;
        set => collectObjectives = value;
    }

    [SerializeField]
    private KillObjective[] killObjectives;
    public KillObjective[] MyKillObjectives
    {
        get => killObjectives;
        set => killObjectives = value;
    }

    [SerializeField]
    private int level;
    public int MyLevel
    {
        get => level;
        set => level = value;
    }

    [SerializeField]
    private int exp;
    public int MyExp
    {
        get => exp;
        set => exp = value;
    }

    [SerializeField]
    private int money;
    public int MyMoney
    {
        get => money;
        set => money = value;
    }

    [SerializeField]
    private Item[] rewardItems;
    public Item[] RewardItems
    {
        get => rewardItems;
        set => rewardItems = value;
    }

    public QuestScript MyQuestScript
    {
        get;set;
    }


    public QuestGiver MyQuestGiver
    {
        get; set;
    }

    public bool IsComplete
    {
        get
        {
            foreach (Objective o in MyCollectObjectives)
            {
                if (!o.IsComplete)
                {
                    return false;
                }
            }
            foreach (Objective k in MyKillObjectives)
            {
                if (!k.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;
    public int MyAmount
    {
        get => amount;
    }

    private int currentAmount;
    public int MyCurrentAmount
    {
        get => currentAmount;
        set => currentAmount = value;
    }
    

    [SerializeField]
    private string type;
    public string MyType
    {
        get => type;
        set => type = value;
    }

    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(Item item)
    {
        if (MyType.ToLower() == item.MyItemName.ToLower())
        {
            MyCurrentAmount = BagController.Instance.GetItemCount(item.MyItemName);
            MyCurrentAmount = BagController.Instance.GetItemCount(MyType);

            QuestController.Instance.CheckCompletion();
            QuestController.Instance.UpdateSelected();
        }
    }

    public void UpdateItemCount()
    {
        MyCurrentAmount = BagController.Instance.GetItemCount(MyType);

        QuestController.Instance.CheckCompletion();
        QuestController.Instance.UpdateSelected();
    }

    public void Complete()
    {
        Stack<Item> items = BagController.Instance.GetItems(MyType, MyAmount);
        foreach (Item item in items)
        {
            item.Remove();
        }
    }
}

[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(Character character)
    {
        if (MyType == character.MyType)
        {
            if (MyCurrentAmount < MyAmount)
            {
                MyCurrentAmount++;
                QuestController.Instance.CheckCompletion();
                QuestController.Instance.UpdateSelected();
            }
        }
    }
}