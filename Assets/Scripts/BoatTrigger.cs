using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatTrigger : MonoBehaviour
{
    public GameObject text;
    private bool _here;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.gameObject.SetActive(true);
        _here = true;
    }

    private void Update()   //if you want this in OnTriggerStay replace GetKeyDown with GetKey
    {
        if (Input.GetKeyDown(KeyCode.E) && _here)
        {
            SceneManager.LoadScene("StartLoc");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.gameObject.SetActive(false);
        _here = false;
    }
}
