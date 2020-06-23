using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance ==null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private int power;
    public int MyPower
    {
        get => power;
        set => power = value;
    }

    [SerializeField]
    private int physical;
    public int MyPhysical
    {
        get => physical;
        set => physical = value;
    }

    [SerializeField]
    private int intellect;
    public int MyIntellect
    {
        get => intellect;
        set => intellect = value;
    }

    [SerializeField]
    private int stamina;
    public int MyStamina
    {
        get => stamina;
        set => stamina = value;
    }

    [SerializeField]
    private int agile;
    public int MyAgile
    {
        get => agile;
        set => agile = value;
    }

    private int mana;
    public int MyMana
    {
        get => mana;
        set => mana = value;
    }

    private int maxMana;
    public int MyMaxMana
    {
        get => maxMana;
        set => maxMana = value;
    }

    [SerializeField]
    private int exp;
    public int MyExp
    {
        get => exp;
        set => exp = value;
    }

    [SerializeField]
    private int lv;
    public int MyLv
    {
        get => lv;
        set => lv = value;
    }

    /// <summary>
    /// 当前交互的内容
    /// </summary>
    public IInteractable MyInteractable
    {
        get;
        set;
    }

    /// <summary>
    /// 限制角色移动的位置
    /// </summary>
    private Vector3 min, max;

    [SerializeField]
    private GameObject[] slashs;
    private int slashIndex = 3;

    public int MyGold
    {
        get;set;
    }

    private void Awake()
    {
        MyHp = MyPhysical * 10;
        MyMaxHp = MyPhysical * 10;

        MyMana = MyIntellect * 10;
        MyMaxMana = MyIntellect * 10;
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected override void Update()
    {
        base.Update();
        if (MainController.Instance.mainWindow.IsVisible())
        {
            MainController.Instance.SetValue(MyHp, MyMaxHp, MyMana, MyMaxMana);
        }
        //角色移动，限制范围
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);

        GetInput();
    }

    private void GetInput()
    {
        Direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Direction += Vector2.up;
            slashIndex = 3;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Direction += Vector2.down;
            slashIndex = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Direction += Vector2.left;
            slashIndex = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Direction += Vector2.right;
            slashIndex = 0;
        }
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Attack();
        }
    }

    private void Attack()
    {
        slashs[slashIndex].SetActive(true);
        slashs[slashIndex].GetComponent<Animator>().SetTrigger("Attack");
    }

    /// <summary>
    /// 设置角色移动范围
    /// </summary>
    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }
}
