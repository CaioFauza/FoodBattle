using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    SteerableBehaviour steerable;
    IShooter shooter;
    public float shootDelay = 0.5f;
    private float _lastShootTimeStamp = 0.0f;

    public override void Awake()
    {
        base.Awake();

        Transition patrol = new Transition();
        patrol.condition = new ConditionDistanceGT(transform, GameObject.FindWithTag("Player").transform, 10.0f);
        patrol.target = GetComponent<PatrolState>();
        transitions.Add(patrol);

        steerable = GetComponent<SteerableBehaviour>();
        shooter = steerable as IShooter;
    }

    public override void Update()
    {   
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        _lastShootTimeStamp = Time.time;
        try
        {
            shooter.Shoot();
        } 
        catch 
        {
            return;
        }
    }
}
