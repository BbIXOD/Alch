using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class Enemy : Entity
{
    public int[] states = new int[3];
    public GameObject[] weapon = new GameObject[3];
    private PotionCollision _pot;
    public NavMeshAgent agent;
    private Transform _pt;
    protected Vector3 Pos;
    public Vector3 myPos;
    protected float Agro, Dist, RSpeed = 2;
    private  Player _player;
    protected bool Spectating = true;

    protected void Awake()
    {
        _pot = GetComponent<PotionCollision>();
        agent = GetComponent<NavMeshAgent>();
        var find = GameObject.Find("Player");
        _pt = find.transform;
        _player = find.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent> ();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
        Pos = _pt.position;
        myPos = transform.position;
        Dist = (Pos - myPos).magnitude;
        agent.speed = speed;
        if (Spectating && Dist <= Agro)
        {
            var ang1 = (Pos - myPos);
            var ang2 = Mathf.Atan2(ang1.y, ang1.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(new Vector3(0f, 0f, ang2)),RSpeed * Time.fixedDeltaTime);
        }
        if (live <= 0) Destroy(gameObject);
        if (Combo(50, 0, 0)) live -= 2;
        if (Combo(10, 10, 10))
        {
            live -= 10;
        }
        if (Combo(0, 10, 5)) live -= 2;
        if (Combo(0, 0, 25))
        {
            if (_pot.Effects.ContainsKey("Potion Frost")) _pot.Effects["Potion Frost"] = _pot.duration;
            else _pot.Effects.Add("Potion Frost", _pot.duration);
            speed -= normalSpeed;
        }

        if (Combo(10, 20, 0))
        {
            live -= 2;
            if (live > 0) return;
            var w = weapon[Random.Range(0, 3)];
            Instantiate(w, myPos, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    protected void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
            _player.GetDamage();
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "Bullet(Clone)") return;
        var e = col.gameObject.GetComponent<PBullet>();
        e.type--;
        states[e.type] += 1;
    }

    protected bool Combo(int r, int g, int b)
    {
        if (r > states[0] || g > states[1] || b > states[2]) return false;
        states[0] -= r;
        states[1] -= g;
        states[2] -= b;
            
        return true;

    }
    public bool SetDest(Vector3 pos, bool range = true)
    {
        if (range && Dist > Agro) return false;
        try
        {
            agent.SetDestination(pos);
        }
        catch (Exception) { /*Debug.LogWarning(e);*/ }
        return true;
    }
}
