using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private float _mana;
    private int _type;
    private RectTransform _rt;
    private Image _image;
    private Weapon _scr;

    private void Start()
    {
        _scr = GameObject.Find("Weapon").GetComponent<Weapon>();
        _rt = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    
    private void Update()
    {
        _mana = _scr.mana;
        _type = _scr.BulletType;
        var r = _rt.transform;
        var transformLocalScale = r.localScale;
        transformLocalScale.x = _mana / 5;
        r.localScale = transformLocalScale;
        _image.color = new Color(_type % 3 % 2, (_type - 1) % 2, (_type % 3 + 1) % 3 % 2, 1);
        
    }
}
