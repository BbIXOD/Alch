using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class Enemy : Entity
{
    public int[] states = new int[3];
    private readonly GameObject[] _weapon = new GameObject[3];
    private GameObject _buff;
    private PotionCollision _pot;
    public NavMeshAgent agent;
    private Transform _pt;
    protected Vector3 Pos;
    public Vector3 myPos;
    protected float Agro, Dist, RSpeed = 2;
    private float _arrMake;
    private  Player _player;
    protected bool Spectating = true;
    private int _buffed;

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
        _weapon[0] = Resources.Load<GameObject>("GunOnGround");
        _weapon[1] = Resources.Load<GameObject>("ShotgunOnGround");
        _weapon[2] = Resources.Load<GameObject>("MortarOnGround");
        _buff = Resources.Load<GameObject>("Buff");
    }

    protected void Update()
    {
        
        //Re.material.color = new Color((float)states[0] / 50, (float)states[1] / 50, (float)states[2] / 50, 1);
        for(var i = 0; i < states.Length; i++)
            if (states[i] > 50)
                states[i] = 50;
        if (_buffed == 0) return;
        _arrMake = MyExtensions.MyExtensions.Check(_arrMake, 10, 15);
        if (_arrMake < 10) return;
        var t = transform;
        var tr = t.localScale;
        var pos = t.position +
                  new Vector3(Random.Range(-1 * tr.x, tr.x), Random.Range(-1 * tr.y, tr.y), 0);
        Instantiate(_buff, pos, Quaternion.Euler(0, 0, 0))
            .transform.SetParent(t);
        
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
        if (Combo(50, 0, 0)) GetDamage(2);
        if (Combo(10, 10, 10))
        {
            GetDamage(10);
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
                return;
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        var aura = col.gameObject.GetComponentInParent<Cool>();
        if (!aura) return;
        aura.obj.Add(this);
        _buffed++;
    }
    
    protected void OnTriggerExit2D(Collider2D col)
    {
        var aura = col.gameObject.GetComponentInParent<Cool>();
        if (!aura) return;
        aura.obj.Remove(this);
        _buffed--;
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

    protected void GetDamage(int damage)
    {
        live -= _buffed == 0 ? damage / 2 : damage;
    }
}
