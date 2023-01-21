using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Entity
{
    public int[] states = new int[3];
    public GameObject[] weapon = new GameObject[3];
    protected PotionCollision Pot;
    protected NavMeshAgent Agent;
    protected Transform Pos;
    protected float Agro;

    protected void Awake()
    {
        Pot = GetComponent<PotionCollision>();
        Agent = GetComponent<NavMeshAgent>();
        Pos = GameObject.Find("Player").transform;
        Agent = GetComponent<NavMeshAgent> ();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    protected void Update()
    {
        //Re.material.color = new Color((float)states[0] / 50, (float)states[1] / 50, (float)states[2] / 50, 1);
        for(var i = 0; i < states.Length; i++)
            if (states[i] > 50)
                states[i] = 50;
    }
    
    protected virtual void FixedUpdate()
    {
        Agent.speed = speed;
        if (live <= 0) Destroy(gameObject);
        if (Combo(50, 0, 0)) live -= 2;
        if (Combo(10, 10, 10))
        {
            live -= 10;
        }
        if (Combo(0, 10, 5)) live -= 2;
        if (Combo(0, 0, 25))
        {
            if (Pot.Effects.ContainsKey("Potion_Frost(Clone)")) Pot.Effects["Potion_Frost(Clone)"] = Pot.duration;
            else Pot.Effects.Add("Potion_Frost(Clone)", Pot.duration);
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
