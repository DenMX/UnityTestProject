using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private float LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 5f;
        LifeTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(LifeTime + 5 <= Time.realtimeSinceStartup)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
