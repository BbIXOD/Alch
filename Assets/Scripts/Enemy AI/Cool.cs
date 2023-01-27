using System.Collections.Generic;
using UnityEngine;

public class Cool : AbstractStriker
{
    public List<Enemy> obj = new List<Enemy>();
    protected override void Start()
    {
        base.Start();
        normalSpeed = 3;
        normalLive = 3;
        Agro = 15;
        Social = 20;
        Aim = false;
        var cc = new GameObject("CoolCol");
        cc.layer = 8;
        var a = cc.AddComponent<CircleCollider2D>();
        a.isTrigger = true;
        a.radius = 10;
        cc.transform.SetParent(transform, false);
    }

    protected override void FixedUpdate()
    {
        if (obj.Count > 0)
        {
            Angle = MyExtensions.MyExtensions.Spectate
                (myPos, obj[Random.Range(0, obj.Count)].myPos);
        }
        else Mana = 0;
        base.FixedUpdate();
            SetDest(Pos);



    }
}
