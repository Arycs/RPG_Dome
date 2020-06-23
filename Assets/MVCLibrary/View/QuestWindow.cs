using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : BaseWindow
{
    public GameObject questPrefab;
    public GameObject questParent;
    public Text questTitle;
    public Button abandonBtn;
    public Button closeBtn;
    public Text questDescription;
    public Text exp;
    public Text money;
    public QuestRewardButton[] items;

    public QuestWindow()
    {
        resName = "UI/Window/QuestWindow";
        resident = true;
        selfType = WindowType.QuestWindow;
        scenesType = ScenesType.Game;
        QuestController.Instance.questWindow = this;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        questPrefab = Resources.Load<GameObject>("Quest");
        questParent = transform.Find("QuestList/Area").gameObject;
        abandonBtn = transform.Find("Abandon").GetComponent<Button>();
        closeBtn = transform.Find("Close").GetComponent<Button>();
        questTitle = transform.Find("Title").GetComponent<Text>();
        questDescription = transform.Find("QuestDescription/Area/Text").GetComponent<Text>();
        exp = transform.Find("Reward/Exp").GetComponent<Text>();
        money = transform.Find("Reward/Money").GetComponent<Text>();
        items = transform.GetComponentsInChildren<QuestRewardButton>();
        base.Awake();
    }

    protected override void OnAddListener()
    {
        base.OnAddListener();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        closeBtn.onClick.AddListener(QuestController.Instance.CloseQuestWindow);
        abandonBtn.onClick.AddListener(QuestController.Instance.AbandonQuest);
        base.RegisterUIEvent();
    }
}
