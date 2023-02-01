using UnityEngine;

public class Dog : Enemy
{
    public AlphaDog leader;
    private float _range;
    protected virtual void Start()
    {
        live = 5;
        normalSpeed = 3;
        Agro = 30;
        _range = Agro;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        myPos = transform.position;
        if (leader)
        {
            if ((leader.transform.position - myPos).magnitude > _range)
                agent.SetDestination(leader.myPos);
            foreach (var dog in FindObjectsOfType<Dog>())
            {
                if ((dog.myPos - myPos).magnitude > _range || dog.leader) continue;
                leader.friends.Add(dog);
                dog.leader = leader;
                leader.pride++;
            }
        }
        else
        {
            SetDest(5 * (myPos - Pos).normalized + myPos);
        }
    }

    private void OnDestroy()
    {
        if (!leader) return;
        leader.friends.Remove(GetComponent<Dog>());
        leader.pride--;
    }
}
