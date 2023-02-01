using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlaySecondScene : MonoBehaviour
{
    public AudioSource Enter;
    public AudioSource Exit;
    bool _here;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enter.Play();
        _here = true;
    }

    private void Update()
    {
        if (_here)
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Exit.Play();
        _here = false;
    }
}
