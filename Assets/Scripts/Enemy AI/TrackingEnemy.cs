
using UnityEngine;

public class TrackingEnemy : Enemy
{
    private void Start()
    {
        normalSpeed = 3;
        live = 10;
        Agro = 50;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((transform.position - Pos.position).magnitude > Agro)
            return;
        Agent.SetDestination(Pos.position);
    }
}
