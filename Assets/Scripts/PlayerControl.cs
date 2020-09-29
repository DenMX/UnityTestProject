using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed; //скорость передвижения
    private Rigidbody2D rb;
    private Vector2 moveVelocity; //направление движения

    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//получение направления

        moveVelocity = moveInput * Speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.Angle(Vector2.right, mousePosition - transform.position) - (transform.position.y < mousePosition.y ? 90 : -90);
        transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mousePosition.y ? angle : -angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)//в случае столкновения смерить
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
