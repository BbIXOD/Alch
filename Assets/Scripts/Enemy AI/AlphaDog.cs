using System.Collections.Generic;

public class AlphaDog : Dog
{
    public int pride = 1;
    private const int BigPride = 6;
    public List<Dog> friends = new List<Dog>();
    protected override void Start()
    {
        base.Start();
        leader = GetComponent<AlphaDog>();
        friends.Add(leader);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        var p = Pos.position;
        
        if (pride < BigPride)
        {
            foreach (var dog in friends)
                dog.agent.SetDestination(5 * (dog.myPos - p).normalized + dog.myPos);
        }
        else 
            foreach (var dog in friends)
                dog.agent.SetDestination(Pos.position);
    }

    private void OnDestroy()
    {
        foreach (var dog in friends)
        {
            dog.leader = null;
        }
    }
}
