using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    //public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            lines = new Queue<DialogueLine>();
            Debug.Log("OK");
        }


    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isDialogueActive)
            {
                DisplayNextDialogueLine();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive =true;

        box.SetActive(true);
        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines) 
        {
            lines.Enqueue(dialogueLine);
        }
        
        DisplayNextDialogueLine();
        Debug.Log(lines.Count);



    }

    public void DisplayNextDialogueLine()
    {
        if(lines.Count ==0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        //characterIcon.sprite = currentLine.character.icon;
        Debug.Log(currentLine.character.name);
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }


    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive=false;
        //animator.Play("hide");
        box.SetActive(false);
    }
}
