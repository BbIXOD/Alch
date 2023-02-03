using UnityEngine;

public class Compas : MonoBehaviour
{
    private GameObject _spect;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.red;
    }

    private void Update()
    {
        if (!_spect) Nearest();
        var transform1 = transform;
        transform.rotation =
            Quaternion.Lerp(transform1.rotation, MyExtensions.MyExtensions.Spectate(transform1.position, _spect.transform.position), 2);
        
    }

    private void Nearest()
    {
        var all = FindObjectsOfType<Enemy>();
        if (all.Length == 0)
        {
            _sr.color = Color.green;
            _spect = GameObject.Find("Boat");
            if (!_spect) _spect = GameObject.Find("Purple Portal");
        }
        var tp = transform.position;
        var magnitude = Mathf.Infinity;
        foreach (var enemy in all)
        {
            var current = ((Vector2)(tp - enemy.myPos)).magnitude;
            if (current > magnitude) continue;
            magnitude = current;
            _spect = enemy.gameObject;
        }
    }
}
