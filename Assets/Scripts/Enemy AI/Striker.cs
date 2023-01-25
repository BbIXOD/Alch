using UnityEngine;

public class Striker : Enemy
{
    private const float AgroMin = 10;
    private float _mana;
    public GameObject bullet;
    private bool _ret;
    private Collider2D _coll;
    private Transform _tr;
    private Vector3 _bPos;
    private void Start()
    {
        normalSpeed = 2.5f;
        live = 3;
        Agro = 20;
        _coll = GetComponent<Collider2D>();
        _tr = transform;
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _mana = MyExtensions.MyExtensions.Check(_mana, 10, 5);
        _ret = Dist >= AgroMin ? SetDest(Pos) :
            SetDest((Vector2)myPos + Vector2.Perpendicular(myPos - Pos));
        if (!_ret) return;
        if (_mana >= 10)
        {
            _mana = 0;
            _bPos = myPos + _tr.TransformDirection(new Vector3(0, 2, 0));
            var a = Instantiate(bullet, _bPos, Quaternion.Euler(_tr.eulerAngles));
            Physics2D.IgnoreCollision(a.GetComponent<Collider2D>(), _coll);
        }
    }
}
