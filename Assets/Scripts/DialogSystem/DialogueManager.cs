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

    private Queue<string> sentences;
    private byte _index;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Dialogue");
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);

        nameText.text = dialogueText.name;
        sentences.Clear();

        
        DisplayNextSentences(dialogue.sentences);
    }

    public void DisplayNextSentences(string[] sentences)
    {
        if(_index == this.sentences.Count)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences[_index];
        _index++;
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
