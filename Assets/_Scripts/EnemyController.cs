using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : SteerableBehaviour, IDamageable, IShooter
{
    GameManager gm;
    private float life = 1.0f;
    public Image healthBar;

    public float shootDelay = 2.5f;
    private float _lastShootTimeStamp = 0.0f;
    public Transform gun;
    public GameObject pizzaEnemy, refrigerantEnemy, donutEnemy, enemyBullet;

    void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        life -= 0.25f;
        healthBar.fillAmount = life;
        gm.points += 20;
        if(life <= 0) Die();
    }

    public void Die()
    {       
        Destroy(gameObject);
    }

    public void Shoot()
    {   
        if(gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        _lastShootTimeStamp = Time.time;
        Instantiate(enemyBullet, gun.position, Quaternion.identity, transform);
    }
}
