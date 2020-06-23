using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseWindow
{
    public Image hpImage;
    public Image mpImage;

    public ActionButton[] actionbuttons;

    public Button characterBtn;
    public Button bagBtn;
    public Button questBtn;
    public Button skillBtn;
    public Button setBtn;

    public GameObject target;
    public Image targetHead;
    public Image targetHp;

    public MainWindow()
    {
        resName = "UI/Window/MainWindow";
        resident = true;
        selfType = WindowType.MainWindow;
        scenesType = ScenesType.Game;
        MainController.Instance.mainWindow = this;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        target = transform.Find("Target").gameObject;
        targetHead = transform.Find("Target/HeadBg/Head").GetComponent<Image>();
        targetHp = transform.Find("Target/Hp").GetComponent<Image>();
        target.SetActive(false);

        hpImage = transform.Find("HeadBg/Bg/HpBg/Image").GetComponent<Image>();
        mpImage = transform.Find("HeadBg/Bg/MpBg/Image").GetComponent<Image>();
        actionbuttons = transform.GetComponentsInChildren<ActionButton>();
        characterBtn = transform.Find("MenuButtons/Character").GetComponent<Button>();
        bagBtn = transform.Find("MenuButtons/Bag").GetComponent<Button>();
        questBtn = transform.Find("MenuButtons/Quest").GetComponent<Button>();
        skillBtn = transform.Find("MenuButtons/Skill").GetComponent<Button>();
        setBtn = transform.Find("MenuButtons/Set").GetComponent<Button>();
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
        base.RegisterUIEvent();
        bagBtn.onClick.AddListener(BagController.Instance.OpenBagWindow);
        questBtn.onClick.AddListener(QuestController.Instance.OpenQuestWindow);
        characterBtn.onClick.AddListener(CharacterController.Instance.OpenCharacterWindow);
    }
}
