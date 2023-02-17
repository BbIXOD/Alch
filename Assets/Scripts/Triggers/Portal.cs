using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && GetComponents<Boss>().Length == 0)
            SceneManager.LoadScene("FinalTitres");
    }
}
