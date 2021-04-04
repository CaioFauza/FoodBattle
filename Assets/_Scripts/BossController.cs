using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossController : SteerableBehaviour, IDamageable, IShooter
{
    GameManager gm;
    public float shootDelay = 1.5f;
    private float _lastShootTimeStamp = 0.0f;
    private float life = 10.0f;
    public Transform gun;
    public Image healthBar;

    public GameObject enemyBullet;
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        life -= 0.25f;
        healthBar.fillAmount = life/10;
        if(life <= 0) Die();
    }

    public void Die()
    {       
        Destroy(gameObject);
    }

    public void Shoot()
    {   
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        _lastShootTimeStamp = Time.time;
        Instantiate(enemyBullet, gun.position, Quaternion.identity, transform);
    }
}
