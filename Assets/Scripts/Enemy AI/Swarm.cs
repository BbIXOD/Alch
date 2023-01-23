
public class Swarm : Enemy
{
    public Swarm leader;
    private Swarm _me;
    public int count = 1;
    public bool alive = true;
    private const int MCount = 5;
    private float _range;
    protected virtual void Start()
    {
        live = 5;
        normalSpeed = 3;
        Agro = 30;
        _range = Agro;
        leader = GetComponent<Swarm>();
        _me = leader;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!leader.alive) leader = _me;
        foreach (var s in FindObjectsOfType<Swarm>())
        { 
            if (s.leader.count > leader.count || s.leader == leader
                || (s.myPos - myPos).magnitude > _range) continue;
            s.leader = leader; 
            leader.count++;
        }

        if ((leader.myPos - myPos).magnitude > _range * 2 && leader.myPos != myPos)
        {
            leader.count--;
            leader = _me;
            count = 1;
        }
        if ((leader.myPos - myPos).magnitude > Agro) SetDest(leader.myPos, false);
        if (leader.count < MCount) SetDest(5 * (myPos - Pos).normalized + myPos);
        else SetDest(Pos);

    }

    private void OnDestroy()
    {
        leader.count--;
        alive = false;
    }
}
