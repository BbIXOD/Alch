using UnityEngine;

public class Player : Entity
{
    private float _v, _h, _angle;
    private Vector3 _pos, _ang;
    private Vector2 _mov;
    private Rigidbody2D _rb;
    private Camera _camera;

    private void Awake()
    {
        live = 1;
        normalSpeed = 250;
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        var t = transform;
        var cPos = _camera.ScreenToWorldPoint(Input.mousePosition) - t.position;
        _angle = Mathf.Atan2(cPos.y, cPos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _v = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        _h = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        _mov = new Vector2(_h, _v);
        _rb.velocity = _mov;
    }
}
