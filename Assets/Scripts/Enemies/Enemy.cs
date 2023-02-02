using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class Enemy : Entity
{
    public int[] states = new int[3];
    
    private readonly GameObject[] _weapon = new GameObject[3];
    private GameObject _buff, _spike;
    
    private PotionCollision _pot;
    public NavMeshAgent agent;
    private Transform _pt;
    private  Player _player;
    private CircleCollider2D _cCol;
    
    protected Vector3 Pos;
    public Vector3 myPos;
    protected float Agro, Dist, RSpeed = 2, Social, Rad;
    private float _arrMake;
    protected bool Spectating = true;
    private bool _sleep = true, _scream = true;
    protected int Buffed;

    protected virtual void Awake()
    {
        _cCol = GetComponent<CircleCollider2D>();
        Rad = _cCol.radius;
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
        _spike = Resources.Load<GameObject>("Spike");
    }

    private void Start()
    {
        speed = normalSpeed;
        live = normalLive;
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
                  new Vector3(Random.Range(-Rad, Rad),
                      Random.Range(-Rad, Rad), 0) * transform.localScale.x;
        Instantiate(_buff, pos, Quaternion.Euler(0, 0, 0))
            .transform.SetParent(t);
        
    }
    
    protected virtual void FixedUpdate()
    {
        Pos = _pt.position;
        myPos = transform.position;
        Dist = (Pos - myPos).magnitude;
        agent.speed = speed;
        if (_sleep)
        {
            if (!_scream)
            {
                Agro /= 2;
                _scream = true;
            }
        }
        _sleep = agent.velocity.magnitude == 0 && Dist > Agro;
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
            if (_pot.Effects.ContainsKey(PotionCollision.Pots.PotionFrost))
                _pot.Effects[PotionCollision.Pots.PotionFrost] = _pot.duration;
            else
            {
                _pot.Effects.Add(PotionCollision.Pots.PotionFrost, _pot.duration);
                speed -= normalSpeed;
            }
        }

        if (Combo(10, 20, 0))
        {
            GetDamage(2);
            if (live > 0) return;
            var w = _weapon[Random.Range(0, 3)];
            Instantiate(w, myPos, Quaternion.Euler(0f, 0f, 0f));
        }

        if (Combo(20, 5, 0) && _pot.Effects.ContainsKey(PotionCollision.Pots.PotionFrost))
        {
            GetDamage(3);
            if (live > 0) return;
            for (var i = 0; i < 4; i++)
            {
                var c = Instantiate(_spike, myPos, Quaternion.Euler(0, 0, 90 * i))
                    .GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(c, _cCol);
            }
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
                if (_scream) WakeUp();
                return;
            case "CoolBullet(Clone)":
                states = new[] { 0, 0, 0 };
                return;
            case "Spike(Clone)":
                GetDamage(2);
                return;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
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
        if (_scream) WakeUp();
        try
        {
            agent.SetDestination(pos);
        }
        catch (Exception) { /*Debug.LogWarning(e);*/ }
        return true;
    }

    private void GetDamage(int damage)
    {
        live -= Buffed == 0 ? damage : damage / 2;
    }

    protected virtual void WakeUp() 
    {
        if (!_scream) return;
        _scream = false;
        Agro *= 2;
    }

    private void OnDestroy()
    {
        agent.enabled = false;
        _player.GetComponent<Throw>().Charge++;
    }
}
