using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    SteerableBehaviour steerable;
    public override void Awake()
    {
        base.Awake();
        Transition attacking = new Transition();
        attacking.condition = new ConditionDistanceLT(transform, GameObject.FindWithTag("Player").transform, 5.0f);
        attacking.target = GetComponent<AttackState>();
        transitions.Add(attacking);
        steerable = GetComponent<SteerableBehaviour>();
    }
}
