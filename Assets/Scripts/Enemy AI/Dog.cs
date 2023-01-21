
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    public Dog leader;
    private Dog _me;
    public int pride;
    private const int BigPride = 10;
    private List<Dog> _friends = new List<Dog>();
    public Vector3 myPos;
    private float _range;
    private void Start()
    {
        live = 5;
        normalSpeed = 2;
        Agro = 70;
        _range = Agro;
        _me = GetComponent<Dog>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        leader = null;
        pride = 1;
        _friends.Clear();
        myPos = transform.position;
        var p = Pos.position;
        if ((myPos - Pos.position).magnitude > Agro)
            return;
        foreach (var dog in GameObject.FindGameObjectsWithTag("Dog"))
        {
            if ((dog.transform.position - myPos).magnitude > _range) continue;
            _friends.Add(dog.GetComponent<Dog>());
            if (!leader)
            {
                _friends[^1].leader = _me;
                pride++;
            }
            else
            {
                _friends[^1].leader = leader;
                leader.pride++;
            }
        }
        if (!leader) return;
        _friends.Add(_me);
        if (pride < BigPride)
        {
            foreach (var dog in _friends)
                dog.Agent.SetDestination(p - myPos * p.magnitude);
        }
        else 
            foreach (var dog in _friends)
                dog.Agent.SetDestination(Pos.position);
    }
}
