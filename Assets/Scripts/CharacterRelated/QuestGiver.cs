using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    /// <summary>
    /// 身上拥有的任务
    /// </summary>
    [SerializeField]
    private Quest[] quests;
    public Quest[] MyQuests
    {
        get => quests;
        set => quests = value;
    }

    #region 任务接取，完成提示等
    /// <summary>
    /// 提示接取任务标志
    /// </summary>
    [SerializeField]
    private Sprite question;

    /// <summary>
    /// 未完成任务标志
    /// </summary>
    [SerializeField]
    private Sprite questionSliver;

    /// <summary>
    /// 完成任务标志
    /// </summary>
    [SerializeField]
    private Sprite exclemation;

    [SerializeField]
    private SpriteRenderer statusRenderer;
    #endregion

    [SerializeField]
    private int questGiverId;

    [SerializeField]
    List<Quest> questInstance = new List<Quest>();
    /// <summary>
    /// 任务实例，因为使用ScriptableObject，防止修改任务。
    /// </summary>
    public List<Quest> MyQuestInstances
    {
        get
        {
            return questInstance;
        }
    }

    private void Awake()
    {
        CreateQuestInstance();
    }

    public void CreateQuestInstance()
    {
        questInstance.Clear();
        foreach (Quest quest in MyQuests)
        {
            if (quest != null)
            {
                Quest tempQ = Instantiate(quest);
                questInstance.Add(tempQ);
            }
        }
    }


    public void UpdateQuestStatue()
    {
        int count = 0;
        foreach (Quest quest in quests)
        {
            if (quest != null)
            {
                if (MyQuestInstances.Find(x => quest.MyQuestName == x.MyQuestName).IsComplete && QuestController.Instance.HasQuest(quest))
                {
                    statusRenderer.sprite = exclemation;
                    break;
                }
                else if (!QuestController.Instance.HasQuest(quest))
                {
                    statusRenderer.sprite = question;
                    break;
                }
                else if (!MyQuestInstances.Find(x => quest.MyQuestName == x.MyQuestName).IsComplete && QuestController.Instance.HasQuest(quest))
                {
                    statusRenderer.sprite = questionSliver;
                }
            }
            else
            {
                count++;
                if (count == MyQuestInstances.Count)
                {
                    dialogNum = 1;
                    statusRenderer.enabled = false;
                }
            }
        }
    }


    public bool canOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canOpen == true)
        {
            DialogController.Instance.ShowDialogWindow(this, dialogDatas[dialogNum]);
        }
    }
}
