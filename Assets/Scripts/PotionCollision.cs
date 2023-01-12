using System;
using UnityEngine;

public class PotionCollision : MonoBehaviour
{
    private Player _entity;
    public GameObject effected;
    private readonly int _duration = 5;

    private void Start()
    {
        _entity = gameObject.name switch
        {
            "Player" => gameObject.GetComponent<Player>(),
            "Enemy" => gameObject.GetComponent<Enemy>(),
            _ => _entity
        };

        _entity.speed = _entity.normalSpeed;
    }

    protected void FixedUpdate()
    {
        if (effected && effected.name == "Potion_Blue")
        {
            _entity.speed *= 2;
            Destroy(effected);
            effected = null;
        }
        if (Math.Abs(_entity.speed - _entity.normalSpeed) < Time.fixedDeltaTime) return;
        if (_entity.speed > _entity.normalSpeed)
            _entity.speed -= Time.fixedDeltaTime * _entity.normalSpeed / _duration;
        else if (_entity.speed < _entity.normalSpeed)
            _entity.speed += Time.deltaTime * _entity.normalSpeed / _duration;
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        effected = other.gameObject;
    }
}    
