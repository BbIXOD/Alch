using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _paused;

    public void ChangeState()
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0 : 1;

    }
}
