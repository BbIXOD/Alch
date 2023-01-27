
public class TrackingEnemy : Enemy
{
    private void Start()
    {
        normalSpeed = 3;
        normalLive = 6;
        Agro = 15;
        Social = Agro;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SetDest(Pos);
    }
}
