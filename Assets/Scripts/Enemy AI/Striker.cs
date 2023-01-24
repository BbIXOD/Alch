using UnityEngine;

public class Striker : Enemy
{
    private const float AgroMin = 10;
    private float _mana;
    public GameObject bullet;
    private bool _ret;
    private Collider2D _coll;
    private void Start()
    {
        normalSpeed = 2.5f;
        live = 3;
        Agro = 20;
        _coll = GetComponent<Collider2D>();
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
            var ang1 = (Pos - myPos);
            var ang2 = Mathf.Atan2(ang1.y, ang1.x) * Mathf.Rad2Deg - 90f;
            var a = Instantiate(bullet, myPos, Quaternion.Euler(0f, 0f, ang2));
            Physics2D.IgnoreCollision(a.GetComponent<Collider2D>(), _coll);
        }
    }
}
