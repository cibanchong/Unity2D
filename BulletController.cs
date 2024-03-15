using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D myrigidbody2D;
    public float bulletSpeed = 10f;
    private Vector3 Direction;
    public GameManager myGameManager;

    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        myGameManager = FindObjectOfType<GameManager>();
        myrigidbody2D.velocity = new Vector2(bulletSpeed, myrigidbody2D.velocity.y);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemGood"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            myGameManager.AddScore();
            Destroy(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        myrigidbody2D.velocity = Direction * bulletSpeed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }
}
