using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotionCollision : MonoBehaviour
{
    private Entity _entity;
    private Weapon _weapon;
    public GameObject effected;
    public Dictionary<object, float> Effects = new Dictionary<object, float>();
    public int duration = 5;
    private string _effectName;
    private object _ePot, _boxer;
    public enum Pots
    {
        PotionBlue,
        PotionFrost,
        GunOnGround,
        ShotgunOnGround,
        MortarOnGround,
    }
    
    protected void Start()
    {
        _entity = MyExtensions.MyExtensions.ToTag(gameObject.name) switch
        {
            "Player" => GetComponent<Player>(),
            "Enemy" => GetComponent<TrackingEnemy>(),
            "Dog" => GetComponent<Dog>(),
            "AlphaDog" => GetComponent<AlphaDog>(),
            "Gnus" => GetComponent<Swarm>(),
            "Striker" => GetComponent<Striker>(),
            "Goblin Boss" => GetComponent<Boss>(),
            "Cool goblin" => GetComponent<Cool>(),
            _ => _entity
        };
        if (_entity.name == "Player") _weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }

    protected void FixedUpdate()
    {
        if (effected)
        {
            _effectName = MyExtensions.MyExtensions.ToTag(effected.name, true);
            try
            {
                _ePot = Enum.Parse(typeof(Pots), _effectName);
            }
            catch (ArgumentException)
            {
                return;
            }
            if (Effects.ContainsKey(_ePot))
            {
                Effects[_ePot] = duration;
                return;
            }
            Effects.Add(_ePot, duration);
            Destroy(effected);
            effected = null;
            switch (_ePot)
            {
                case Pots.PotionBlue:
                    _entity.speed += _entity.normalSpeed;
                    return;
                case Pots.PotionFrost:
                    _entity.speed -= _entity.normalSpeed / 2;
                    return;
                case Pots.GunOnGround:
                    if (_weapon) _weapon.weaponType = "Gun";
                    Effects.Remove(_effectName);
                    return;
                case Pots.ShotgunOnGround:
                    if (_weapon) _weapon.weaponType = "ShotGun";
                    Effects.Remove(_effectName);
                    return;
                case Pots.MortarOnGround:
                    if (_weapon) _weapon.weaponType = "Mortar";
                    Effects.Remove(_effectName);
                    return;
            }
        }

        if (Effects.Count == 0)
        {
            return;
        }
        
        if (Effects.ContainsKey(Pots.PotionBlue) && Effects.ContainsKey(Pots.PotionFrost))
        {
            Effects.Remove(Pots.PotionBlue);
            Effects.Remove(Pots.PotionFrost);
            _entity.speed = _entity.normalSpeed;
        }
        foreach (var el in Effects.Keys.ToArray())
        {
            Effects[el] -= Time.fixedDeltaTime;
            if (Effects[el] <= 0)
            {
                Effects.Remove(el);
                switch (el)
                {
                    case Pots.PotionBlue:
                        _entity.speed -= _entity.normalSpeed;
                        return;
                    case Pots.PotionFrost:
                        _entity.speed += _entity.normalSpeed / 2;
                        return;
                }
            }
        }

        
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_entity.live <= 0) return;
        effected = other.gameObject;
    }
}    
