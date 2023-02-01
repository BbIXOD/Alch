using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BorderTrigger : MonoBehaviour
{
    public GameObject text;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        text.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        text.SetActive(false);
    }


}
