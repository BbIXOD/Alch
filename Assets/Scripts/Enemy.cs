using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class Enemy : Entity
{
    public int[] states = new int[3];
    private readonly GameObject[] _weapon = new GameObject[3];
    private GameObject _buff, _shout;
    private PotionCollision _pot;
    public NavMeshAgent agent;
    private Transform _pt;
    protected Vector3 Pos;
    public Vector3 myPos;
    protected float Agro, Dist, RSpeed = 2, Social;
    private float _arrMake, _dist;
    private  Player _player;
    protected bool Spectating = true;
    private bool _sleep = true, _scream = true;
    protected int Buffed;

    protected void Awake()
    {
        _dist = GetComponent<CircleCollider2D>().radius;
        _pot = GetComponent<PotionCollision>();
        agent = GetComponent<NavMeshAgent>();
        var find = GameObject.Find("Player");
        _pt = find.transform;
        _player = find.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent> ();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _weapon[0] = Resources.Load<GameObject>("GunOnGround");
        _weapon[1] = Resources.Load<GameObject>("ShotgunOnGround");
        _weapon[2] = Resources.Load<GameObject>("MortarOnGround");
        _buff = Resources.Load<GameObject>("Buff");
        _shout = Resources.Load<GameObject>("Shout");
    }

    protected void Update()
    {
        
        //Re.material.color = new Color((float)states[0] / 50, (float)states[1] / 50, (float)states[2] / 50, 1);
        for(var i = 0; i < states.Length; i++)
            if (states[i] > 50)
                states[i] = 50;
        if (Buffed == 0) return;
        _arrMake = MyExtensions.MyExtensions.Check(_arrMake, 10, 15);
        if (_arrMake < 10) return;
        var t = transform;
        var pos = t.position +
                  new Vector3(Random.Range(-_dist, _dist),
                      Random.Range(-_dist, _dist), 0) * transform.localScale.x;
        Instantiate(_buff, pos, Quaternion.Euler(0, 0, 0))
            .transform.SetParent(t);
        
    }
    
    protected virtual void FixedUpdate()
    {
        Pos = _pt.position;
        myPos = transform.position;
        Dist = (Pos - myPos).magnitude;
        agent.speed = speed;
        _sleep = Dist > Agro && agent.destination == myPos;
        if (_sleep) _scream = true;
        if (Spectating && Dist <= Agro)
        {
            var ang1 = (Pos - myPos);
            var ang2 = Mathf.Atan2(ang1.y, ang1.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(new Vector3(0f, 0f, ang2)),RSpeed * Time.fixedDeltaTime);
        }
        if (live <= 0) Destroy(gameObject);
        if (Combo(50, 0, 0)) GetDamage(2);
        if (Combo(10, 10, 10))
        {
            GetDamage(5);
        }
        if (Combo(0, 10, 5)) GetDamage(2);
        if (Combo(0, 0, 25))
        {
            if (_pot.Effects.ContainsKey("Potion Frost")) _pot.Effects["Potion Frost"] = _pot.duration;
            else _pot.Effects.Add("Potion Frost", _pot.duration);
            speed -= normalSpeed;
        }

        if (Combo(10, 20, 0))
        {
            GetDamage(2);
            if (live > 0) return;
            var w = _weapon[Random.Range(0, 3)];
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
        switch (col.gameObject.name)
        {
            case "Bullet(Clone)":
                var e = col.gameObject.GetComponent<PBullet>();
                e.type--;
                states[e.type] += 1;
                return;
            case "CoolBullet(Clone)":
                states = new[] { 0, 0, 0 };
                if (!_sleep) return;
                _sleep = false;
                Agro *= 2;
                return;
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Scream")
            SetDest(Pos, false);
        
        var aura = col.gameObject.GetComponentInParent<Cool>();
        if (!aura) return;
        aura.obj.Add(this);
        Buffed++;
    }
    
    protected void OnTriggerExit2D(Collider2D col)
    {
        var aura = col.gameObject.GetComponentInParent<Cool>();
        if (!aura) return;
        aura.obj.Remove(this);
        Buffed--;
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
        if (_scream)
        {
            _scream = false;
            Instantiate(_shout, myPos + new Vector3(0, _dist, 0), Quaternion.Euler(0,  0, 0), transform);
            var s = new GameObject("Scream")
            {
                transform =
                {
                    position = myPos // What is this?
                }
            };
            s.AddComponent<Shout>();
            var cc = s.AddComponent<CircleCollider2D>();
            cc.radius = Social;
            cc.isTrigger = true;
        }
        try
        {
            agent.SetDestination(pos);
        }
        catch (Exception) { /*Debug.LogWarning(e);*/ }
        return true;
    }

    private void GetDamage(int damage)
    {
        live -= Buffed == 0 ? damage / 2 : damage;
    }
}
