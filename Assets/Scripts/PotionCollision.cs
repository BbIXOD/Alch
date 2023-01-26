using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotionCollision : MonoBehaviour
{
    private Entity _entity;
    private Weapon _weapon;
    public GameObject effected;
    public Dictionary<string, float> Effects = new Dictionary<string, float>();
    public int duration = 5;
    private string _effectName;
    private string[] _pots = { "Potion Blue", "Potion Frost", "GunOnGround", "ShotgunOnGround", "MortarOnGround"};
    private void Start()
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

        _entity.speed = _entity.normalSpeed;
        if (_entity.name == "Player") _weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }

    protected void FixedUpdate()
    {
        if (effected)
        {
            _effectName = MyExtensions.MyExtensions.ToTag(effected.name);
            if (!_pots.Contains(_effectName)) return;
            if (Effects.ContainsKey(_effectName)) Effects[_effectName] = duration;
            else Effects.Add(_effectName, duration);
            Destroy(effected);
            effected = null;
            switch (_effectName)
            {
                case "Potion Blue":
                    _entity.speed += _entity.normalSpeed;
                    return;
                case "Potion Frost":
                    _entity.speed -= _entity.normalSpeed;
                    return;
                case "GunOnGround":
                    if (_weapon) _weapon.weaponType = "Gun";
                    Effects.Remove(_effectName);
                    return;
                case "ShotgunOnGround":
                    if (_weapon) _weapon.weaponType = "ShotGun";
                    Effects.Remove(_effectName);
                    return;
                case "MortarOnGround":
                    if (_weapon) _weapon.weaponType = "Mortar";
                    Effects.Remove(_effectName);
                    return;
            }
        }

        if (Effects.Count == 0)
        {
            return;
        }

        if (Effects.ContainsKey("Potion Blue") && Effects.ContainsKey("Potion Frost"))
        {
            Effects.Remove("Potion Blue");
            Effects.Remove("Potion Frost");
            _entity.speed = _entity.normalSpeed;
        }
            
        if (Effects.ContainsKey("Potion Blue"))
        {
            _entity.speed -= Time.fixedDeltaTime * _entity.normalSpeed / duration;
        }
        if (Effects.ContainsKey("Potion Frost"))
        {
            _entity.speed += Time.fixedDeltaTime * _entity.normalSpeed / duration;
        }
        
        foreach (var el in Effects.Keys.ToArray())
        {
            Effects[el] -= Time.fixedDeltaTime;
            if (Effects[el] <= 0) Effects.Remove(el);
        }

        
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (_entity.live <= 0) return;
        if (gameObject.name == "CoolCol") return;
        effected = other.gameObject;
    }
}    
