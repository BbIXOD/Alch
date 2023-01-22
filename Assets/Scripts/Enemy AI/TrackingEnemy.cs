
public class TrackingEnemy : Enemy
{
    private void Start()
    {
        normalSpeed = 3;
        live = 10;
        Agro = 15;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if ((transform.position - Pos).magnitude > Agro)
            return;
        agent.SetDestination(Pos);
    }
}
