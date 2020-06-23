using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : BaseWindow
{
    public Text lvNum;

    public Text damage;
    public Text power;

    public Text health;
    public Text physical;

    public Text mana;
    public Text intelligence;

    public Text armor;
    public Text stamina;

    public Text critical;
    public Text agile;
    public Button closeBtn;

    public CharButton[] charButtons;

    public CharacterWindow()
    {
        resName = "UI/Window/CharacterWindow";
        resident = true;
        selfType = WindowType.CharacterWindow;
        scenesType = ScenesType.Game;
        CharacterController.Instance.characterWindow = this;
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        charButtons = transform.GetComponentsInChildren<CharButton>();
        lvNum = transform.Find("Title/LevelNum/Text").GetComponent<Text>();
        damage = transform.Find("State/Damage").GetComponent<Text>();
        power = transform.Find("State/Damage/Power").GetComponent<Text>();
        health = transform.Find("State/Health").GetComponent<Text>();
        physical = transform.Find("State/Health/Physical").GetComponent<Text>();
        mana = transform.Find("State/Mana").GetComponent<Text>();
        intelligence = transform.Find("State/Mana/Intelligence").GetComponent<Text>();
        armor = transform.Find("State/Armor").GetComponent<Text>();
        stamina = transform.Find("State/Armor/Stamina").GetComponent<Text>();
        critical = transform.Find("State/Critical").GetComponent<Text>();
        agile = transform.Find("State/Critical/Agile").GetComponent<Text>();
        closeBtn = transform.Find("Close").GetComponent<Button>();

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
        closeBtn.onClick.AddListener(CharacterController.Instance.CloseCharacterWindow);
    }

}
