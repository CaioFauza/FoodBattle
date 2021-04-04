using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : SteerableBehaviour
{
  private Vector3 direction;

  private void OnTriggerEnter2D(Collider2D collision)
  {
      if (collision.CompareTag("Enemy")) return;
      if(collision.CompareTag("Floor")) Destroy(gameObject);

      IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
      if (!(damageable is null))
      {
          damageable.TakeDamage();
      }
      Destroy(gameObject);
  }

  void Start()
  {
      Vector3 posPlayer = GameObject.FindWithTag("Player").transform.position;
      direction = (posPlayer - transform.position).normalized;
  }

  void Update()
  {
      Thrust(direction.x*2, direction.y);
  }

  private void OnBecameInvisible()
  {
      gameObject.SetActive(false);
  }
}
