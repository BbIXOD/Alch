using System.Threading;
using UnityEngine;

public class Player : Entity
{
    private float _v, _h, _angle, _shield = 1;
    private Vector3 _pos, _ang;
    private Vector2 _mov;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Camera _camera;

    private void Awake()
    {
        live = 3;
        normalSpeed = 250;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (_shield < 1) Recover();
        var t = transform;
        var cPos = _camera.ScreenToWorldPoint(Input.mousePosition) - t.position;
        _angle = Mathf.Atan2(cPos.y, cPos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _v = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        _h = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        _mov = new Vector2(_h, _v);
        _rb.velocity = _mov;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "EBullet(Clone)") GetDamage();
    }

    public void GetDamage()
    {
        if (_shield < 1) return;
        live--;
        if (live < 1) Destroy(gameObject);
        speed += 200;
        _shield = 0;
    }

    public void Recover()
    {
        
        _shield = MyExtensions.MyExtensions.Check(_shield, 1, 1);
        _sr.enabled = !_sr.enabled;
        if (_shield <= 1) return;
        speed -= 200;
        _sr.enabled = true;
    }
}
