using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;

    public Animator boxAnim;
<<<<<<< HEAD
    //public Animator startAnim;
    public GameObject startAnim;

    private Queue<string> sentences;


    private void Start()
    {
        sentences = new Queue<string>();
    }
=======
    public Animator startAnim;
    
    private short _index;
>>>>>>> parent of ffd4095 (working dialog system)

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Function is owrking");
        boxAnim.SetBool("boxOpen", true);
        //startAnim.SetBool("startOpen", false);
        startAnim.SetActive(false);

        nameText.text = dialogue.name;
        _index = -1;
        
        DisplayNextSentences(dialogue.sentences);
    }

    public void DisplayNextSentences(string[] sentences)
    {
        _index++;
        if(_index == sentences.Length)
        {
            EndDialogue();
            return;
        }
        var sentence = sentences[_index];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    
    private void DisplayPreviousSentences(string[] sentences)
    {
        _index--;
        if(_index < 0)
        {
            EndDialogue();
            return;
        }
        var sentence = sentences[_index];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(var letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void EndDialogue()
    {
        boxAnim.SetBool("boxOpen", false);
    }
}
