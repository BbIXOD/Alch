using UnityEngine;

public class Swarm : Enemy
{
    public Swarm leader;
    private Swarm _me;
    public Vector3 myPos;
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
        myPos = transform.position;
        foreach (var s in FindObjectsOfType<Swarm>())
        { 
            if (s.leader.count > leader.count || s.leader == leader
                || (s.myPos - myPos).magnitude > _range) continue;
            s.leader = leader; 
            leader.count++;
        }

        if ((leader.myPos - myPos).magnitude > _range / 1.25f && leader.myPos != myPos)
        {
            leader.count--;
            leader = _me;
            count = 1;
        }
        if ((leader.myPos - myPos).magnitude > Agro) agent.SetDestination(leader.myPos);
        if ((Pos - myPos).magnitude > Agro) return; 
        if (leader.count < MCount) agent.SetDestination(5 * (myPos - Pos).normalized + myPos);
        else agent.SetDestination(Pos);

    }

    private void OnDestroy()
    {
        leader.count--;
        alive = false;
    }
}
