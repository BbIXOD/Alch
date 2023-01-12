using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    private float _trajectory;
    private float _speed = 10f;
    private Vector3 _position;
    public int type;
    private string _wType;
    private Transform _trans;
    private Renderer _ren;
    private void Start()
    {
        type = GameObject.Find("Weapon").GetComponent<Weapon>().BulletType;
        _wType = GameObject.Find("Weapon").GetComponent<Weapon>().weaponType;
        _ren = GetComponent<SpriteRenderer>();
        _trans = transform;
        _ren.material.color = new Color(type % 3 % 2, (type - 1) % 2, (type % 3 + 1) % 3 % 2, 1);
        _position = _trans.position;
        if (_wType == "Mortar")
        {
            _position += new Vector3(0, 0, 1) * Random.Range(7, 10);
            _trajectory = _position.z / 2;
            _speed *= _position.z;
            _speed /= 2;
        }
        else
        {
            _speed *= 10;
            transform.localScale *= 2;
        }
        transform.position = _position;
        
    }

    private void Update()
    {
        _ren.material.color -= (Color)(new Vector4(0, 0, 0, 3) * Time.deltaTime);
        if (_ren.material.color.a <= 0)
        {
            Debug.Log(transform.position.z);
            Destroy(gameObject);
        }
        if (_wType == "Mortar")
        {
            if (_position.z <= 0) return;
            _position = _trans.position;
            _trans.localScale += new Vector3(1, 1, 0) * ((_position.z - _trajectory) * Time.deltaTime);
            _trans.Translate(new Vector3(0, 0, -_trajectory * 60) * (Time.deltaTime));
        }
        _trans.Translate(new Vector3(0, 1 * _speed, 0) * (Time.deltaTime));
    }
}
