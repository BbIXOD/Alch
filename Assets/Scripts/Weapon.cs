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
        mana = Check(mana, 10, 10);
        _cooldown = Check(_cooldown, 1, 9f);
        if (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetMouseButtonDown(1))
        {
            BulletType++;
            if (BulletType == 4) BulletType = 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            BulletType--;
            if (BulletType == -1) BulletType = 3;
        }

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

    private static float Check(float var, int maxValue, float mult)
    {
        if (var >= maxValue) return maxValue;
        
        return var + Time.deltaTime * mult;
    }

    private void FireMortar()
    {
        if (mana < 10) return;
        for (byte el = 0; el <= mana; el++)
        {
            _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-15, 16));
            Instantiate(bullet, transform.position, Quaternion.Euler(_ang));
        }
        mana = 0;
    }

    private void FireGun()
    {
        if (mana < 1.5 || _cooldown < 1) return;
        _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-1, 2));
        Instantiate(bullet, transform.position, Quaternion.Euler(_ang));
        mana -= 1.5f;
        _cooldown = 0;
    }

    private void FireShotGun()
    {
        if (mana < 10) return;
        for (byte el = 0; el <= mana; el++)
        {
            _ang = transform.eulerAngles + new Vector3(0, 0, Random.Range(-15, 16));
            Instantiate(bullet, transform.position, Quaternion.Euler(_ang));
        }
        mana = 0;
    }
}
