using UnityEngine;

public class Boss : Enemy
{
    private float _mana, _spin, _mass;
    private const float Power = 350; 
    private Rigidbody2D _prb;
    private void Start()
    {
        normalSpeed = 2.5f;
        live = 15;
        Agro = 17;
        _prb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        _mass = _prb.mass;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_spin > 0)
        {
            transform.Rotate(new Vector3(0, 0, 20));
            _prb.AddForce( _mass * Power * (myPos - Pos).normalized);
            _spin -= Time.fixedDeltaTime;
            return;
        }
        if (!SetDest(Pos)) return;
        Spectating = true;
        _mana = MyExtensions.MyExtensions.Check(_mana, 10, 1);
        if (_mana < 10) return;
        _mana = 0;
        _spin = 10;
        Spectating = false;
        SetDest(Pos);
    }
}
