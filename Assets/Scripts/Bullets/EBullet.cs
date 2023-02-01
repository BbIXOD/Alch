

using UnityEngine;

public class EBullet : Bullet
{
    [SerializeField] private float speed = 500;
    private void Start()
    {
        Speed = speed;
    }
}    
