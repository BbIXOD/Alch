
using UnityEngine;

public class TrackHero : MonoBehaviour
{
    private Transform _player;
    private Vector3 _oldPos, _newPos;
    private const float Speed = 3f;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    private void FixedUpdate()
    {
        _oldPos = transform.position;
        _newPos = _player.position;
        if (gameObject.CompareTag("MainCamera"))
        {
            _oldPos.z = -10f;
            _newPos.z = -10f;
        }
        transform.position = Vector3.Lerp(_oldPos, _newPos, Speed*Time.fixedDeltaTime);
    }
}
