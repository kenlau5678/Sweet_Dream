using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    //public Sprite icon; // Uncomment or use if you have icons for characters
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool hasTriggered = false; // This will keep track of whether the dialogue has been triggered

    public void TriggerDialogue()
    {
        if (!hasTriggered) // Check if the dialogue has not been triggered before
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            hasTriggered = true; // Set the flag to true to prevent re-triggering
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // It's better to use CompareTag instead of directly accessing the tag property
        {
            TriggerDialogue();
        }
    }
}
