using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossController : SteerableBehaviour, IDamageable, IShooter
{
    GameManager gm;
    float shootDelay = 1.0f;
    private float _lastShootTimeStamp = 0.0f;
    private float life = 10.0f;
    public Transform gun;
    public Image healthBar;
    public GameObject enemyBullet;
    public AudioClip bossSFX, firstStageSFX, finalStageSFX, wonSFX;
    Text speedUpText, finalStageText;

    bool followPlayer = false;
    bool firstSFX = false;
    void Start()
    {
        gm = GameManager.GetInstance();
        speedUpText = GameObject.Find("UI_SpeedUpText").GetComponent<Text>();
        finalStageText = GameObject.Find("UI_FinalStageText").GetComponent<Text>();
        speedUpText.gameObject.SetActive(false);
        finalStageText.gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        life -= 0.25f;
        healthBar.fillAmount = life/10;

        if(life == 5.0f) {
            AudioManager.PlaySFX(firstStageSFX);
            shootDelay = 0.5f;
            speedUpText.gameObject.SetActive(true);
            StartCoroutine(DelayRemove(speedUpText.gameObject));   
        } else if(life == 2.0f){
            AudioManager.PlaySFX(finalStageSFX);
            finalStageText.gameObject.SetActive(true);
            StartCoroutine(DelayRemove(finalStageText.gameObject)); 
            followPlayer = true;
        } else if(life <=0){
            Die();
        }
    }

    IEnumerator DelayRemove(GameObject obj){
        yield return(new WaitForSeconds(2));
        obj.SetActive(false);
    }

    public void Update()
    {
        if(followPlayer){
            Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
            Vector2 playerDirection =  (playerPosition - transform.position).normalized;
            Thrust(playerDirection.x, playerDirection.y);
        }

        if(!firstSFX && Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) <= 10.0f)
        {
             AudioManager.PlaySFX(bossSFX);
             firstSFX = true;
        }
    }
    public void Die()
    {   
        AudioManager.PlaySFX(wonSFX);
        gm.ChangeState(GameManager.GameState.END);
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
