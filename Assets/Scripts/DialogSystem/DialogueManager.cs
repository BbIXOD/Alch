using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;

    public Animator boxAnim;
    public Animator startAnim;
    
    private short _index;

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Function is owrking");
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

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
