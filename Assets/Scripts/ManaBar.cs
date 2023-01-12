using UnityEngine;

public class ManaBar : MonoBehaviour
{
    private float _mana;
    private const float PosX = -10;
    private int _type;
    private SpriteRenderer _re;
    private Weapon _scr;

    private void Start()
    {
        _scr = GameObject.Find("Weapon").GetComponent<Weapon>();
        _re = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        var t = transform;
        var pos = t.position;
        _mana = _scr.mana;
        _type = _scr.BulletType;
        t.localScale = new Vector3(_mana, 1, 1);
        t.position = new Vector3(PosX, pos.y, pos.z);
        _re.color = new Color(_type % 3 % 2, (_type - 1) % 2, (_type % 3 + 1) % 3 % 2, 1);
        
    }
}
