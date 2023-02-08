
public class Swarm : Goblin
{
    public Swarm leader;
    private Swarm _me;
    public int count = 1;
    public bool alive = true;
    private const int MCount = 5;
    protected override void Awake()
    {
        base.Awake();
        normalLive = 4;
        normalSpeed = 3;
        Agro = 11;
        Social = Agro;
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
                || (s.myPos - myPos).magnitude > Social) continue;
            s.leader = leader; 
            leader.count++;
        }

        if ((leader.myPos - myPos).magnitude > Social * 2 && leader.myPos != myPos)
        {
            leader.count--;
            leader = _me;
            count = 1;
        }
        if ((leader.myPos - myPos).magnitude > Social) SetDest(leader.myPos, false);
        if (leader.count < MCount && Buffed == 0) SetDest(5 * (myPos - Pos).normalized + myPos);
        else SetDest(Pos);

    }

    private void OnDestroy()
    {
        leader.count--;
        alive = false;
    }
}
