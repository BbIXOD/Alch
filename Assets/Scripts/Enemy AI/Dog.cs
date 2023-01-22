using UnityEngine;

public class Dog : Enemy
{
    public AlphaDog leader;
    public Vector3 myPos;
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
            if ((leader.transform.position - myPos).magnitude > _range / 2)
                agent.SetDestination(leader.transform.position);
            foreach (var dog in GameObject.FindGameObjectsWithTag("Dog"))
            {
                if ((dog.transform.position - myPos).magnitude > _range) continue;
                leader.friends.Add(dog.GetComponent<Dog>());
                if (leader.friends[^1].leader) return;
                leader.friends[^1].leader = leader;
                leader.pride++;
                Debug.Log("Catch");
            }
        }
        else
        {
            agent.SetDestination(5 * (myPos - Pos.position).normalized + myPos);
        }
    }

    private void OnDestroy()
    {
        if (!leader) return;
        leader.friends.Remove(GetComponent<Dog>());
        leader.pride--;
    }
}
