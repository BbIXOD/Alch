using System.Collections.Generic;
using UnityEngine;

public class Cool : AbstractStriker
{
    public GameObject cc;
    public List<Enemy> obj = new List<Enemy>();
    protected override void Start()
    {
        base.Start();
        normalSpeed = 3;
        live = 17;
        Agro = 15;
        Aim = false;
        Instantiate(cc, myPos, Quaternion.Euler(0, 0, 0));

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
