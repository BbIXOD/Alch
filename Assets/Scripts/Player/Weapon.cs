using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public float mana;
    private float _cooldown;
    public string weaponType = "Mortar";
    [SerializeField] private GameObject bullet;
    private Vector3 _ang;
    [NonSerialized] public int BulletType = 1;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetMouseButtonDown(1))
        {
            if (BulletType >= 3) BulletType = 1;
            else BulletType++;
        }
        else
        {
            if (BulletType <= 1) BulletType = 3;
            else BulletType--;
        }
    }


    private void FixedUpdate()
    {
        mana = MyExtensions.MyExtensions.Check(mana, 10, 10);
        _cooldown = MyExtensions.MyExtensions.Check(_cooldown, 1, 9);

        if (!Input.GetMouseButton(0)) return;
        switch (weaponType)
        {
            case "Mortar": FireMortar();
                return;
            case "Gun": FireGun();
                return;
            case "ShotGun": FireShotGun();
                return;
        }
    }

    private void FireMortar()
    {
        if (mana < 10) return;
        for (byte el = 0; el <= mana; el++)
        {
            _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-15, 16));
            var a = Instantiate(bullet, transform.position, Quaternion.Euler(_ang)).GetComponent<PBullet>();
            a.wType = weaponType;
            a.type = BulletType;
        }
        mana = 0;
    }

    private void FireGun()
    {
        if (mana < 1.5 || _cooldown < 1) return;
        _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-1, 2));
        var a = Instantiate(bullet, transform.position, Quaternion.Euler(_ang)).GetComponent<PBullet>();
        a.wType = weaponType;
        a.type = BulletType;
        mana -= 1.5f;
        _cooldown = 0;
    }

    private void FireShotGun()
    {
        if (mana < 10) return;
        for (byte el = 0; el <= mana; el++)
        {
            _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-15, 16));
            var a = Instantiate(bullet, transform.position, Quaternion.Euler(_ang)).GetComponent<PBullet>();
            a.wType = weaponType;
            a.type = BulletType;
        }
        mana = 0;
    }
}
