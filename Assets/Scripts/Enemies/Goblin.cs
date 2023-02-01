using UnityEngine;

public class Goblin : Enemy
{
    private GameObject _shout;

    protected override void Awake()
    {
        base.Awake();
        _shout = Resources.Load<GameObject>("Shout");
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Scream")
            SetDest(Pos, false);
    }
    

    protected override void WakeUp()
    {
        base.WakeUp();
        var t = transform;
        Instantiate(_shout, myPos + t.TransformDirection(new Vector3(0, Rad, 0)), Quaternion.Euler(t.eulerAngles), transform);
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
}
