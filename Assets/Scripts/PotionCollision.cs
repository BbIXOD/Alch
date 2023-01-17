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

    private void Start()
    {
        _entity = gameObject.name switch
        {
            "Player" => gameObject.GetComponent<Player>(),
            "Enemy" => gameObject.GetComponent<Enemy>(),
            _ => _entity
        };

        _entity.speed = _entity.normalSpeed;
        if (_entity.name == "Player") _weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }

    protected void FixedUpdate()
    {
        if (effected)
        {
            Effects.Add(effected.name, duration);
            _effectName = effected.name;
            Destroy(effected);
            effected = null;
            switch (_effectName)
            {
                case "Potion_Blue(Clone)":
                    _entity.speed *= 2;
                    return;
                case "Potion_Frost(Clone)":
                    _entity.speed = 0;
                    return;
                case "GunOnGround(Clone)":
                    if (_weapon) _weapon.weaponType = "Gun";
                    Effects.Remove(_effectName);
                    return;
                case "ShotgunOnGround(Clone)":
                    if (_weapon) _weapon.weaponType = "ShotGun";
                    Effects.Remove(_effectName);
                    return;
                case "MortarOnGround(Clone)":
                    if (_weapon) _weapon.weaponType = "Mortar";
                    Effects.Remove(_effectName);
                    return;
            }
        }

        if (Effects.Count == 0)
        {
            return;
        }

        if (Effects.ContainsKey("Potion_Blue(Clone)") && Effects.ContainsKey("Potion_Frost(Clone)"))
        {
            Effects.Remove("Potion_Blue(Clone)");
            Effects.Remove("Potion_Frost(Clone)");
            _entity.speed = _entity.normalSpeed;
        }
            
        if (Effects.ContainsKey("Potion_Blue(Clone)"))
        {
            _entity.speed -= Time.fixedDeltaTime * _entity.normalSpeed / duration;
        }
        if (Effects.ContainsKey("Potion_Frost(Clone)"))
        {
            _entity.speed += Time.deltaTime * _entity.normalSpeed / duration;
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
        if(other.name == "Bullet(Clone)") return;
        effected = other.gameObject;
    }
}    
