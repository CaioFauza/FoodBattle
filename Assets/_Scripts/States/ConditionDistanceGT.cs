using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDistanceGT : Condition
{
   Transform agent;
   Transform target;
   float minDistance;

   public ConditionDistanceGT(Transform _agent, Transform _target, float _distance)
   {
       agent = _agent;
       target = _target;
       minDistance = _distance;
   }

   public override bool Test()
   {
       return Vector2.Distance(agent.position, target.position) >= minDistance;
   }
}
