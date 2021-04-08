using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    GameManager gm;
    public List<Transition> transitions;

    public virtual void Awake()
    {
        gm = GameManager.GetInstance();
        transitions = new List<Transition>();
    }
    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void Update()
    {
    }

    public void LateUpdate()
    {
       if(gm.gameState != GameManager.GameState.GAME) return;
       foreach (Transition t in transitions) {
           if (t.condition.Test()) {
               t.target.enabled = true;
               this.enabled = false;
               return;
           }
       }
   }
}
