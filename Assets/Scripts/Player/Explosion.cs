using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D _cCol;
    private void Awake()
    {
        _cCol = GetComponent<CircleCollider2D>();
        _cCol.enabled = false;
        StartCoroutine(Boom());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (!enemy) return;
        col.gameObject.GetComponent<Enemy>().live -= 3;
    }

    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        _cCol.enabled = true;
        yield return new WaitForSeconds(Time.fixedDeltaTime * 5);
        Destroy(gameObject);
    }
}
