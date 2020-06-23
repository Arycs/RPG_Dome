﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        parent.Direction = Vector2.zero;
    }

    public void Update()
    {
        if (parent.MyTarget != null)
        {
            parent.Direction = (parent.MyTarget.transform.position - parent.transform.position).normalized;

            parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyTarget.transform.position, parent.MySpeed * Time.deltaTime);

            float disatance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (disatance <= parent.MyAttackRange)
            {
                parent.ChangeState(new AttackState());
            }
        }
        if (!parent.InRange)
        {
            parent.ChangeState(new EvadeState());
        }
    }
}
