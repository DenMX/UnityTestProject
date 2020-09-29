using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float Speed;
    public GameObject Shard;
    private Rigidbody2D rb;
    private float Life;
    
    public AudioClip _Destroy;


    void Start()//Получаем компонент rigidbody и задаем направление движения в сторону игрока на момент появления астероида
    {
        rb = GetComponent<Rigidbody2D>();
        
        var angle = Vector2.Angle(Vector2.right, GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) - (transform.position.y < GameObject.FindGameObjectWithTag("Player").transform.position.y ? 90 : -90);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < GameObject.FindGameObjectWithTag("Player").transform.position.y ? angle : -angle);
        rb.velocity = transform.up * Speed;
        Life = Time.realtimeSinceStartup;
        
    }

    
    void Update()//ограничиваем время жизни для сохранения космоса чистым
    {
        if(Life+30 < Time.realtimeSinceStartup)
        {
            Destroy(gameObject);
        }
    }

    public void Die()//эмулируем раскалывание астероида на кусочки
    {
        AudioSource.PlayClipAtPoint(_Destroy, transform.position);
        var shard1 = Instantiate(Shard, transform.position, transform.rotation);
        
        var angle1 = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - 5);
        var shard2 = Instantiate(Shard, transform.position, Quaternion.Euler(angle1));
        
        var angle2 = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5);
        var shard3 = Instantiate(Shard, transform.position, Quaternion.Euler(angle2));
        
        Destroy(gameObject);
    }
}
