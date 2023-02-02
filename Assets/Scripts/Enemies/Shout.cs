using UnityEngine;

public class Shout : MonoBehaviour
{
    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.75) Destroy(gameObject);
    }
}
