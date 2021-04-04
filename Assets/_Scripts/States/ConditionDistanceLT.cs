using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDistanceLT : Condition
{
   Transform agent;
   Transform target;
   float maxDistance;

   public ConditionDistanceLT(Transform _agent, Transform _target, float _distance)
   {
       agent = _agent;
       target = _target;
       maxDistance = _distance;
   }

   public override bool Test()
   {
       return Vector2.Distance(agent.position, target.position) <= maxDistance;
   }
}
