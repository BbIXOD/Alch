using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PBullet : Bullet
{
    private float _trajectory;
    private Vector3 _position;
    public int type;
    [FormerlySerializedAs("_wType")] public string wType;
    private Renderer _ren;
    private CircleCollider2D _ck;
    private void Start()
    {
        _ck = GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), _ck);
        Speed = 4000f;
        _ren = GetComponent<SpriteRenderer>();
        _ren.material.color = new Color(type % 3 % 2, (type - 1) % 2, (type % 3 + 1) % 3 % 2, 1);
        if (wType == "Mortar")
        {
            _ck.enabled = false;
            _position = Tr.position;
            _position += new Vector3(0, 0, 1) * Random.Range(8, 13);
            _trajectory = _position.z / 2;
            Speed *= _position.z;
            Speed *= 0.05f;
            Tr.position = _position;
        }

    }

    protected override void FixedUpdate()
    {
        _ren.material.color -= (Color)(new Vector4(0, 0, 0, 3) * Time.fixedDeltaTime);
        if (_ren.material.color.a <= 0) Destroy(gameObject);
        if (wType == "Mortar")
        {
            if (_position.z <= 2 && !_ck.enabled) _ck.enabled = true;
            if (_position.z <= 0) return;
            base.FixedUpdate();
            _position = Tr.position;
            Tr.localScale += new Vector3(1, 1, 0) * ((_position.z - _trajectory) * Time.fixedDeltaTime);
            Tr.Translate(new Vector3(0, 0, -_trajectory * 10) * Time.fixedDeltaTime);
        }
        else base.FixedUpdate();
    }
}
