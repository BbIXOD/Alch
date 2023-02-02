using System.Collections;
using UnityEngine;

public class Shems : Enemy
{
    [SerializeField]private GameObject js;
    [SerializeField] private AudioSource speak;
    private Collider2D _col;
    protected override void Awake()
    {
        base.Awake();
        normalSpeed = 4;
        normalLive = 1;
        Agro = 20;
        Social = Agro;
        _col = GetComponent<Collider2D>();
        Spectating = false;
        StartCoroutine(Pew());
        speak.Play();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        SetDest(Pos);
    }

    private IEnumerator Pew()
    {
        var b = Instantiate(js, myPos,
            MyExtensions.MyExtensions.Spectate(myPos, Pos)).GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(b, _col);

        for (var i = 0; i < 360; i += 45)
        {
            b = Instantiate(js, myPos,
                Quaternion.Euler(0, 0, i)).GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(b, _col);
        }
        
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Pew());
    }
}
