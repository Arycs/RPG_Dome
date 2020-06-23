using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;
    //攻击冷却
    private float attackCooldown = 3;

    private float extraRange = 0.1f;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    void IState.Update()
    {
        if (parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance >= parent.MyAttackRange + extraRange && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
            else
            {
                var player = parent.MyTarget.GetComponent<Character>().gameObject;
                
                parent.MyTarget.GetComponent<Character>().TakeDamage(5, parent.transform);
                //击退效果
                player.transform.position -= (parent.transform.position - parent.MyTarget.GetComponent<Character>().gameObject.transform.position).normalized / 2;
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

}
