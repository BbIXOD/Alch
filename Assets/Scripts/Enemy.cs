using UnityEngine;

public class Enemy : Player
{
    protected int[] State = new int[3];
    protected int X, Y, Live = 10;
    protected float V, H;
    protected Vector3 Mov;
    protected Rigidbody2D Rb;
    protected SpriteRenderer Re;
    protected GameObject Player;

    protected void Awake()
    {
        normalSpeed = 20;
        Rb = GetComponent<Rigidbody2D>();
        Re = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
    }

    protected void Update()
    {
        Re.material.color = new Color((float)State[0] / 50, (float)State[1] / 50, (float)State[2] / 50, 1);
    }
    
    protected void FixedUpdate()
    {
        if (Live <= 0) Destroy(gameObject);
        X = Player.transform.position.x > transform.position.x ? 1 : -1;
        Y = Player.transform.position.y > transform.position.y ? 1 : -1;
        H = X * speed * Time.fixedDeltaTime;
        V = Y * speed * Time.fixedDeltaTime;
        Mov = new Vector3(H, V, 0);
        Rb.velocity = transform.TransformDirection(Mov);
        if (Combo(50, 0, 0)) Live -= 2;
        if (Combo(0, 10, 5)) Live -= 2;
        if (Combo(0, 0, 25)) speed = 0;
        if (Combo(10, 10, 10))
        {
            Live -= 10;
        }
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
            Destroy(col.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Bullet(Clone)" ||
            collision.gameObject.GetComponent<Transform>().position.z > 2) return;
        var col = collision.gameObject;
        var e = collision.gameObject.GetComponent<Bullet>();
        e.type--;
        State[e.type] += 1;
        Destroy(col);
    }

    protected bool Combo(int r, int g, int b)
    {
        if (r <= State[0] && g <= State[1] && b <= State[2])
        {
            State[0] -= r;
            State[1] -= g;
            State[2] -= b;
            
            return true;
        }

        return false;
    }
}
