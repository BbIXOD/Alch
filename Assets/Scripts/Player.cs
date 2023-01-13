using UnityEngine;

public class Player : MonoBehaviour
{
    private float _v, _h, _angle; 
    public float speed, normalSpeed = 150;
    public int live = 1;
    private Vector3 _pos, _ang, _mov;
    private Rigidbody2D _rb;
    private Camera _camera;

    private void Start()
    {
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
        _mov = new Vector3(_h, _v, 0);
        _rb.velocity = _mov;
    }
}
