using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _paused;

    [SerializeField] private GameObject[] buttons = new GameObject[3];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ChangeState();
    }

    public void ChangeState()
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0 : 1;
        foreach (var button in buttons)
            button.SetActive(_paused);
    }
}
