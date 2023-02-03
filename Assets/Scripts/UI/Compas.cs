using UnityEngine;

public class Compas : MonoBehaviour
{
    private Enemy _spect;
    private void Update()
    {
        if (! _spect || _spect.live <= 0) Nearest();
        var transform1 = transform;
        transform.rotation =
            Quaternion.Lerp(transform1.rotation, MyExtensions.MyExtensions.Spectate(transform1.position, _spect.myPos), 2);
    }

    private void Nearest()
    {
        var tp = transform.position;
        var magnitude = Mathf.Infinity;
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            var current = ((Vector2)(tp - enemy.myPos)).magnitude;
            if (current > magnitude) continue;
            magnitude = current;
            _spect = enemy;
        }
    }
}
