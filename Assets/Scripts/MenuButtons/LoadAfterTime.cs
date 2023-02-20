using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterTime : MonoBehaviour
{
    [SerializeField] private string location;
    [SerializeField] private float time;
    private void Awake()
    {
        StartCoroutine(NextLoc());
    }

    private IEnumerator NextLoc()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(location);
    }
}
