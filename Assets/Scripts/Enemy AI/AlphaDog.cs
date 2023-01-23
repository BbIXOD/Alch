using System.Collections.Generic;
using UnityEngine;

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
        if (Dist > Agro) return;
        if (pride < BigPride)
        {
            foreach (var dog in friends)
                dog.SetDest(5 * (dog.myPos - Pos).normalized + dog.myPos, false);
        }
        else 
            foreach (var dog in friends)
                dog.SetDest(Pos, false);
    }

    private void OnDestroy()
    {
        foreach (var dog in friends)
        {
            dog.leader = null;
        }
    }
}
