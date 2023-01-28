using UnityEngine;

public class Striker : AbstractStriker
{
    private const float AgroMin = 10;
    private Vector3 _p;
    protected override void Awake()
    {
        base.Awake();
        normalSpeed = 2.5f;
        normalLive = 3;
        Agro = 20;
        Social = 10;
        Mult = 5;
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _p = Dist >= AgroMin ? Pos :
            (Vector2)myPos + Vector2.Perpendicular(myPos - Pos);
        SetDest(_p);
    }
}
