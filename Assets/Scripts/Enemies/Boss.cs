using UnityEngine;

public class Boss : Goblin
{
    private float _mana, _spin, _mass;
    private const float Power = 250; 
    private Rigidbody2D _prb;
    public GameObject minion;
    protected override void Awake()
    {
        base.Awake();
        normalSpeed = 2.5f;
        normalLive = 10;
        Agro = 17;
        Social = 10;
        _prb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        _mass = _prb.mass;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_spin > 0)
        {
            SetDest(myPos);
            transform.Rotate(new Vector3(0, 0, 20));
            _prb.AddForce( _mass * Power * (myPos - Pos).normalized);
            _spin -= Time.fixedDeltaTime * 2;
            return;
        }
        if (!SetDest(Pos)) return;
        Spectating = true;
        _mana = MyExtensions.MyExtensions.Check(_mana, 10, 1);
        if (_mana < 10) return;
        _mana = 0;
        if (live > 6)
        {
            _spin = 10;
            Spectating = false;
            SetDest(Pos);
        }
        else
        {
            var tr = transform;
            var v = myPos + 4 * tr.localScale.x * Rad * Random.onUnitSphere;
            v.z = 1;
            Instantiate(minion, v, Quaternion.Euler(tr.eulerAngles));
        }    
    }
}
