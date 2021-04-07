using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : SteerableBehaviour
{
    GameManager gm;
    private float direction;
    
    void Start()
    {
        gm = GameManager.GetInstance();
        direction = GameObject.FindWithTag("Player").transform.localScale.x;
    }

    void Update()
    {   
        if(gm.gameState != GameManager.GameState.GAME) return;
        Thrust(direction * 2, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player")) return;
       if(collision.CompareTag("Floor")) Destroy(gameObject);
       IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
       if (!(damageable is null))
       {
           damageable.TakeDamage();
       }
       Destroy(gameObject);
   }
}
