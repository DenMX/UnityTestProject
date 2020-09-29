using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int Health = 99;
    public float Speed;
    public Transform firePosition;
    public GameObject Bullet;
    private GameObject Player;
    private float time;
    
    public AudioClip _Destroy;
   
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        time = Time.realtimeSinceStartup;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time + 3 < Time.realtimeSinceStartup && Player != null)
        {
            Instantiate(Bullet, firePosition.position, firePosition.rotation).GetComponent<Rigidbody2D>().velocity =
            (GameObject.FindGameObjectWithTag("Player").transform.position * (Speed * 2f));
            time = Time.realtimeSinceStartup;
        }
        
    }

    private void FixedUpdate()
    {
        if(Player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed);
            var angle = Vector2.Angle(Vector2.right, Player.transform.position - transform.position) - (transform.position.y < Player.transform.position.y ? 90 : -90);
            transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < Player.transform.position.y ? angle : -angle);
        }
    }

    public void ApplyDamage(int damage)
    {
        if(Health > damage)
        {
            Health -= damage;
        }
        else
        {
            Die();
        }

    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(_Destroy, transform.position);
        Destroy(gameObject);
    }
}
