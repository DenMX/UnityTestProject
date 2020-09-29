using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    private float LifeTime;

    public AudioClip _Destroy;

    
    void Start()
    {
        LifeTime = Time.realtimeSinceStartup;
        GetComponent<Rigidbody2D>().velocity = transform.up * 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LifeTime + 5 <= Time.realtimeSinceStartup)
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(_Destroy, transform.position);
        Destroy(gameObject);
    }
}
