
using UnityEngine;

public class SoundPlaySecondScene : MonoBehaviour
{
    public AudioSource enter; 
    public AudioSource exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        enter.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        exit.Play();
    }
}
