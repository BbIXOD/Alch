
using UnityEngine;

public class SoundPlaySecondScene : MonoBehaviour
{
    public AudioSource enter; 
    public AudioSource exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enter.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exit.Play();
    }
}
