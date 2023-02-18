using UnityEngine;

public class Player : Entity
{
    private float _v, _h, _angle, _shield = 1;
    private Vector3 _pos, _ang;
    private Vector2 _mov;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Camera _camera;
    [SerializeField] private GameObject compas;

    private void Awake()
    {
        normalLive = 3;
        normalSpeed = normalSpeed == 0 ? 400 : normalSpeed;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        speed = normalSpeed;
        live = normalLive;
        Instantiate(compas, Vector3.zero, Quaternion.Euler(0, 0, 0))
            .transform.SetParent(transform, false);
    }

    private void Update()
    {
        _v = Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime;
        _h = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (_shield < 1) Recover();
        var t = transform;
        var cPos = _camera.ScreenToWorldPoint(Input.mousePosition) - t.position;
        _angle = Mathf.Atan2(cPos.y, cPos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _mov = new Vector2(_h, _v);
        _rb.velocity = _mov;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("DamagingPlayer")) GetDamage();
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
