using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private float hp;
    public float MyHp
    {
        get => hp;
        set => hp = value;
    }

    [SerializeField]
    private float maxHp;
    public float MyMaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    [SerializeField]
    private float speed;
    public float MySpeed
    {
        get => speed;
    }

    [SerializeField]
    private string type;
    public string MyType
    {
        get => type;
    }
    
    [SerializeField]
    private int level;
    public int Level
    {
        get => level;
        set => level = value;
    }

    public Animator MyAnimator;

    [SerializeField]
    private Rigidbody2D myRigidbdoy;

    /// <summary>
    /// 被击中范围判定
    /// </summary>
    [SerializeField]
    protected Transform hitBox;

    private Vector2 direction;
    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    public bool IsAttacking
    {
        get;set;
    }

    public bool IsMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;
        }
    }

    public bool IsAlive
    {
        get
        {
            return MyHp > 0;
        }
    }

    public Transform MyTarget
    {
        get; set;
    }

    protected virtual void Awake()
    {
        MyAnimator = GetComponent<Animator>();
        myRigidbdoy = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleLayers();
    }

    public virtual void Move()
    {
        if (IsAlive)
        {
            myRigidbdoy.velocity = Direction.normalized * MySpeed;
        }
    }

    public void HandleLayers()
    {
        if (IsAlive)
        {
            if (IsMoving)
            {
                ActiveLayer("WalkLayer");
                MyAnimator.SetFloat("X", Direction.x);
                MyAnimator.SetFloat("Y", Direction.y);
            }
            else
            {
                ActiveLayer("IdleLayer");
            }
        }
        else
        {
            ActiveLayer("DeathLayer");
        }
    }

    public void ActiveLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void TakeDamage(int damage, Transform source)
    {
        hp -= damage;
        //Camera.main.GetComponent<CameraController>().CallShake();
        Camera.main.GetComponent<CameraController>().CameraShake(0.1f);
        if (hp <= 0)
        {
            Direction = Vector2.zero;
            myRigidbdoy.velocity = direction;
            MyAnimator.SetTrigger("die");
            GameManager.Instance.OnKillConfirmed(this);
            if (this is Enemy)
            {
                //TODO 角色长经验.
                this.GetComponent<Enemy>().OnCharacterRemoved();
            }
        }
    }

}
