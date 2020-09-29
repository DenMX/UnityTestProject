using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10;
    public int damage = 100;
    private Rigidbody2D rb;
    public float lifeLimit = 3;
    private float Life = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        Life = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        
        if (Life + 1.5f < Time.realtimeSinceStartup)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Asteroid asteroid = collision.GetComponent<Asteroid>();
        Shard shard = collision.GetComponent<Shard>();                
        if (enemy != null)
        {
            enemy.Die();
            GameManager.Score = 2;
        }
        if(asteroid != null)
        {
            asteroid.Die();
            GameManager.Score = 1;
        }
        if (shard)
        {
            shard.Die();
            GameManager.Score = 1;
        }
        Destroy(gameObject);
    }

}
