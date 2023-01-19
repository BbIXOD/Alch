using UnityEngine;

public class Enemy : Entity
{
    public int[] states = new int[3];
    protected int X, Y;
    protected float V, H;
    protected Vector3 Mov;
    protected Rigidbody2D Rb;
    protected SpriteRenderer Re;
    protected GameObject Player;
    public GameObject[] weapon = new GameObject[3];
    protected PotionCollision Pot;

    protected void Awake()
    {
        normalSpeed = 20;
        live = 10;
        Rb = GetComponent<Rigidbody2D>();
        Re = GetComponent<SpriteRenderer>();
        Player = GameObject.Find("Player");
        Pot = gameObject.GetComponent<PotionCollision>();
    }

    protected void Update()
    {
        Re.material.color = new Color((float)states[0] / 50, (float)states[1] / 50, (float)states[2] / 50, 1);
    }
    
    protected void FixedUpdate()
    {
        if (live <= 0) Destroy(gameObject);
        X = Player.transform.position.x > transform.position.x ? 1 : -1;
        Y = Player.transform.position.y > transform.position.y ? 1 : -1;
        H = X * speed * Time.fixedDeltaTime;
        V = Y * speed * Time.fixedDeltaTime;
        Mov = new Vector3(H, V, 0);
        Rb.velocity = transform.TransformDirection(Mov);
        if (Combo(50, 0, 0)) live -= 2;
        if (Combo(10, 10, 10))
        {
            live -= 10;
        }
        if (Combo(0, 10, 5)) live -= 2;
        if (Combo(0, 0, 25))
        {
            Pot.Effects.Add("Potion_Frost(Clone)", Pot.duration);
            speed = 0;
        }

        if (Combo(10, 20, 0))
        {
            live -= 2;
            if (live > 0) return; 
            var tr = transform;
            var w = weapon[Random.Range(0, 3)];
            Instantiate(w, tr.position, Quaternion.Euler(tr.eulerAngles));
        }
        for(var i = 0; i < states.Length; i++)
            if (states[i] > 50)
                states[i] = 50;
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
        states[e.type] += 1;
        Destroy(col);
    }

    protected bool Combo(int r, int g, int b)
    {
        if (r > states[0] || g > states[1] || b > states[2]) return false;
        states[0] -= r;
        states[1] -= g;
        states[2] -= b;
            
        return true;

    }
}
