using UnityEngine;

public abstract class AbstractStriker : Goblin
{
    public GameObject bullet;
    protected float Mult = 1;
    protected float Mana;
    private Collider2D _coll;
    private Vector3 _bPos;
    protected Quaternion Angle;
    protected bool Aim = true;

    protected override void Awake()
    {
        base.Awake();
        _coll = GetComponent<Collider2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Dist > Agro) return;
        Mana = MyExtensions.MyExtensions.Check(Mana, 10, Mult);
        if (Mana < 10) return;
        var ang = Aim ? Quaternion.Euler(transform.eulerAngles) : Angle;
        Mana = 0;
        var tr = transform;
        _bPos = myPos + tr.TransformDirection(new Vector3(0, 2, 0));
        var a = Instantiate(bullet, _bPos, ang);
        Physics2D.IgnoreCollision(a.GetComponent<Collider2D>(), _coll);
    }
}
