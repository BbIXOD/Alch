using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float Speed;
    protected Transform Tr;
    private Rigidbody2D _rb;
    private void Awake()
    {
        Tr = transform;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void FixedUpdate()
    {
        _rb.velocity = Tr.TransformDirection(new Vector3(0, Time.fixedDeltaTime * Speed, 0));
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
    
}
