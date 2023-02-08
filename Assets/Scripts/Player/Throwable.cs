using UnityEngine;

public class Throwable : Bullet
{
    private float _dist, _fallDist;
    private Vector3 _target;
    [SerializeField] private GameObject effect;
    private void Start()
    {
        Speed = 300;
        var tp = transform.position;
        _target = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        _fallDist = ((Vector2)(tp - _target)).magnitude * 0.5f;
        transform.rotation = MyExtensions.MyExtensions.Spectate(tp, _target);
    }

    protected override void FixedUpdate()
    {
        var tr = transform;
        base.FixedUpdate();
        _dist = ((Vector2)(tr.position - _target)).magnitude;
        tr.localScale += (Vector3)((_dist - _fallDist) * Time.fixedDeltaTime * 4 / _fallDist * Vector2.one);
        if (_dist < 0.1f)
        {
            Instantiate(effect, tr.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
        
    }
}
