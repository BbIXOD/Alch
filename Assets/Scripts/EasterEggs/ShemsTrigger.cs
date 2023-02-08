using UnityEngine;

public class ShemsTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shems;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        Instantiate(shems, transform.position + new Vector3(0, 3, 0), Quaternion.Euler(0, 0, 0))
            .GetComponent<Shems>().enabled = true;
    }
}
