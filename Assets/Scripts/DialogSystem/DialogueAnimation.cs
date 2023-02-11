using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimation : MonoBehaviour
{
    //public Animator startAnim;
    public GameObject startAnim;
    public DialogueManager dm;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        startAnim.SetActive(true);
        //startAnim.SetBool("startOpen", true);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        startAnim.SetActive(false);
        Debug.Log("Exit Dialogue");
        //startAnim.SetBool("startOpen", false);
        dm.EndDialogue();
    }
}
