using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float playerJumpForce = 20f;
    public float playerSpeed = 5f;
    public Sprite[] mySprites;
    private int index = 0;
    private bool Grounded;
    private Animator Animator;
    public GameObject BulletPrefab;
    private float Horizontal;
    private Rigidbody2D myrigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private float LastShoot;
    public GameManager myGameManager;
    private int Health = 5;


    private readonly int walkingHash = Animator.StringToHash("walking");
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WalkCoRoutine());
        Animator = GetComponent<Animator>();
        myGameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        if (Animator != null)
        {
            Animator.SetBool("walking", Horizontal != 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Q) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    void FixedUpdate()
    {
        myrigidbody2D.velocity = new Vector2(Horizontal * playerSpeed, myrigidbody2D.velocity.y);

        if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;
    }

    IEnumerator WalkCoRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            mySpriteRenderer.sprite = mySprites[index];
            index++;
            if (index == 6)
            {
                index = 0;
            }
        }
    }

    private void Jump()
    {
        myrigidbody2D.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
        Grounded = false;
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemGood"))
        {
            Destroy(collision.gameObject);
            myGameManager.AddScore();
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            PlayerDeath();
        }
        else if (collision.CompareTag("DeathZone"))
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene("Level2D");
    }
}
