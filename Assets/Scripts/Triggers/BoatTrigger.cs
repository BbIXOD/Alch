using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatTrigger : MonoBehaviour
{
    private GameObject _text;
    private TextMeshProUGUI _textMesh;
    private bool _here;
    [SerializeField] private string location;

    private void Awake()
    {
        _text = GameObject.Find("BoatText");
        _text.SetActive(false);
        _textMesh = _text.GetComponent<TextMeshProUGUI>();
        _textMesh.text = "Press E to leave";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _text.SetActive(true);
        _here = true;
    }

    private void Update()   //if you want this in OnTriggerStay replace GetKeyDown with GetKey
    {
        if (Input.GetKeyDown(KeyCode.E) && _here)
        {
            if (GetComponents<Enemy>().Length == 0)
            {
                StopAllCoroutines();
                StartCoroutine(TempText());
                return;
            }
            SceneManager.LoadScene(location);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _text.SetActive(false);
        _here = false;
    }

    private IEnumerator TempText()
    {
        _textMesh.text = "You must kill all goblins to leave";
        yield return new WaitForSeconds(3);
        _textMesh.text = "Press E to leave";
    }
}
