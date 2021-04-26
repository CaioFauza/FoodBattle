using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    GameManager gm;
    Animator animator;
    Rigidbody2D rigidBody;

    public Transform gun;
    public GameObject bullet;
    public float shootDelay = 0.5f;
    private float _lastShootTimeStamp = 0.0f;
    bool isGrounded = true;
    int speed;
    Text warningText;

    public AudioClip shootSFX, damageSFX, gameOverSFX, warningSFX;
    
    private void Start()
    {
       animator = GetComponent<Animator>();
       gm = GameManager.GetInstance();
       rigidBody = GetComponent<Rigidbody2D>();
       speed = 9;
       warningText = GameObject.Find("UI_WarningText").GetComponent<Text>();
       warningText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        float inputX = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetKeyDown(KeyCode.UpArrow);

        animator.SetFloat("Speed", inputX != 0 ? 1.0f : 0.0f);
        animator.SetFloat("Jump", jumpInput ? 1.0f : 0.0f);

        Vector3 playerDirection = transform.localScale;
        Vector3 bulletDirection = bullet.transform.localScale;
        if(inputX < 0){
            playerDirection.x = -1;
            bulletDirection.x = -3;
            
        }  else if(inputX > 0) {
            playerDirection.x = 1;
            bulletDirection.x = 3;
        }
        
        transform.localScale = playerDirection;
        bullet.transform.localScale = bulletDirection;

        transform.position += new Vector3(inputX, 0, 0) * Time.deltaTime * speed;

        if(jumpInput && isGrounded) rigidBody.AddForce(new Vector2(0, 9.0f), ForceMode2D.Impulse);

        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) gm.ChangeState(GameManager.GameState.PAUSE);

        if (Input.GetKeyDown(KeyCode.Space)) Shoot(); 

        if(transform.position.y < -5.0f) {
            transform.position = new Vector3(-93.41f, -1.38f, 0);
            gm.lifes = 0;
            Die();
        }
    }

    public void Shoot()
    {   
        if(gm.gameState != GameManager.GameState.GAME) return;
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);
        _lastShootTimeStamp = Time.time;
        Instantiate(bullet, gun.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor") isGrounded = true;
        if (collision.collider.tag == "Enemy" && gm.gameState == GameManager.GameState.GAME) TakeDamage();
        if(collision.collider.tag == "Warning" && !gm.warningStatus){
            AudioManager.PlaySFX(warningSFX);
            warningText.gameObject.SetActive(true);
            StartCoroutine(DelayRemove(warningText.gameObject));
            gm.warningStatus = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor") isGrounded = false;
    }

     public void TakeDamage()
    {  
        AudioManager.PlaySFX(damageSFX);
        gm.lifes--;
        if ((gm.lifes <= 0) && (gm.gameState == GameManager.GameState.GAME)) Die();
    }

     public void Die() {
        animator.SetFloat("Speed", 0.0f);
        animator.SetFloat("Jump", 0.0f);
        transform.position = new Vector3(-93.41f, -1.38f, 0);
        AudioManager.PlaySFX(gameOverSFX);
        gm.ChangeState(GameManager.GameState.END);
    }

    IEnumerator DelayRemove(GameObject obj){
        yield return(new WaitForSeconds(2));
        obj.SetActive(false);
    }

}
