using UnityEngine;

public class Striker : Enemy
{
    private const float AgroMin = 5;
    private float _mana;
    public GameObject bullet;
    private void Start()
    {
        normalSpeed = 2.5f;
        live = 3;
        Agro = 15;
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MyExtensions.MyExtensions.Check(_mana, 10, 5);
        if (Dist >= AgroMin) SetDest(Pos);
        if (_mana >= 10)
        {
            _mana = 0;
            Instantiate(bullet, myPos, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
