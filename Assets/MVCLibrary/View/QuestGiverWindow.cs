using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : BaseWindow
{
    public Button closeBtn;
    public Button acceptBtn;
    public Button completeBtn;
    public Button backBtn;
    public Text questDescription;
    public GameObject questArea;
    public GameObject questPrefab;

    public QuestGiverWindow()
    {
        resName = "UI/Window/QuestGiverWindow";
        resident = true;
        selfType = WindowType.QuestGiverWindow;
        scenesType = ScenesType.Game;
        QuestGiverController.Instance.questGiverWindow = this;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        questPrefab = Resources.Load<GameObject>("QuestGiverQuest");
        closeBtn = transform.Find("Close").GetComponent<Button>();
        acceptBtn = transform.Find("AcceptBtn").GetComponent<Button>();
        completeBtn = transform.Find("CompleteBtn").GetComponent<Button>();
        backBtn = transform.Find("BackBtn").GetComponent<Button>();
        questDescription = transform.Find("Content/Description").GetComponent<Text>();
        questArea = transform.Find("Content/Area").gameObject;

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
        acceptBtn.onClick.AddListener(QuestGiverController.Instance.Accept);
        backBtn.onClick.AddListener(QuestGiverController.Instance.Back);
        completeBtn.onClick.AddListener(QuestGiverController.Instance.CompleteQuest);
        closeBtn.onClick.AddListener(QuestGiverController.Instance.CloseQuestGiverWindow);
        base.RegisterUIEvent();
    }
}
