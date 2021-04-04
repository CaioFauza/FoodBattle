using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    GameManager gm;
    Animator animator;
    public float speed;
    Rigidbody2D rigidBody;

    public Transform gun;
    public GameObject bullet;
    public float shootDelay = 0.5f;
    private float _lastShootTimeStamp = 0.0f;
    // public AudioClip shootSFX;
    bool isGrounded = true;
    
    private void Start()
    {
       speed = 9; 
       animator = GetComponent<Animator>();
       gm = GameManager.GetInstance();
       rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        transform.position += new Vector3(inputX, 0, 0) * Time.deltaTime * speed;

        if(Input.GetAxisRaw("Vertical") != 0 && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, 20.0f) * Time.deltaTime * speed, ForceMode2D.Impulse);
        }

        if(Input.GetAxisRaw("Jump") != 0){ Shoot(); }
    }

    public void Shoot()
    {
        if(Time.time - _lastShootTimeStamp < shootDelay) return;
        //AudioManager.PlaySFX(shootSFX);
        _lastShootTimeStamp = Time.time;
        Instantiate(bullet, gun.position, Quaternion.identity, transform);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")  isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor") isGrounded = false;
    }

     public void TakeDamage()
    {
    }

     public void Die() { Destroy(gameObject); }

}
