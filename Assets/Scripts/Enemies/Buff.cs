using MyExtensions;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(0, Time.deltaTime, 0);
        _sr.color = _sr.color.SetAlpha(_sr.color.a - Time.deltaTime);
    }
}
