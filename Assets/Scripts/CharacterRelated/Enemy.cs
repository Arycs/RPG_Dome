using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character, IInteractable
{
    public event HealthChanged HealthChanged;

    public event CharacterRemoved CharacterRemoved;

    private IState currentState;

    [SerializeField]
    private Image hpImage;

    public float MyAttackRange
    {
        get; set;
    }

    public float MyAggroRange
    {
        get; set;
    }

    public Vector3 MyStartPosition
    {
        get; set;
    }


    [SerializeField]
    [Header("头像")]
    private Sprite head;
    public Sprite MyHead
    {
        get => head;
    }

    [SerializeField]
    private float initAggroRange;

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    public float dieTime;

    private void Awake()
    {
        MyAnimator = GetComponent<Animator>();
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 1;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            currentState.Update();
            hpImage.fillAmount = MyHp / MyMaxHp;
        }
        else
        {
            dieTime += Time.deltaTime;
            hpImage.gameObject.SetActive(false);
            MainController.Instance.HideTargetFrame();
            if (dieTime > 5)
            {
                Destroy(gameObject);
                dieTime = 0;
            }
        }
        base.Update();
    }

    public override void TakeDamage(int damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);
            OnHealthChanged(MyHp);
        }
    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public Transform Select()
    {
        //TODO 显示敌人 在 主面板 血条
        HealthChanged += new HealthChanged(MainController.Instance.UpdateTargetFrame);
        CharacterRemoved += new CharacterRemoved(MainController.Instance.HideTargetFrame);
        MainController.Instance.ShowTargetFrame(this);
        hpImage.gameObject.SetActive(true);
        return transform;
    }

    public void DeSelect()
    {
        //TODO 隐藏敌人 在 主面板 血条
        hpImage.gameObject.SetActive(false);
        MainController.Instance.HideTargetFrame();
        //移除血量检测事件
        HealthChanged -= new HealthChanged(MainController.Instance.UpdateTargetFrame);
        CharacterRemoved -= new CharacterRemoved(MainController.Instance.HideTargetFrame);
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;

        currentState.Enter(this);
    }

    public void Reset()
    {
        this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHp = this.MyMaxHp;
        OnHealthChanged(MyMaxHp);
    }




    public void OnHealthChanged(float health)
    {
        HealthChanged?.Invoke(health);
    }

    public void OnCharacterRemoved()
    {
        Debug.Log("调用");
        CharacterRemoved?.Invoke();
        // Destroy(gameObject);
    }

    public void Interact()
    {

    }

    public void StopInteract()
    {

    }
}
