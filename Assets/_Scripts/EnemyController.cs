using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable
{
    GameManager gm;
    private float life = 1.0f;
    public Image healthBar;
    void Start()
    {
        gm = GameManager.GetInstance();
    }

    public void TakeDamage()
    {
        life -= 0.25f;
        healthBar.fillAmount = life;
        if(life <= 0) Die();
    }

    public void Die()
    {       
        Destroy(gameObject);
    }
}
