using System.Collections.Generic;
using _Game.Scripts.DialogueSystem;
using UnityEngine;

public class DialogueSystemTest : MonoBehaviour
{
    public TextAsset dialogueJsonFile;
    private List<DialogueData> allDialogues;
    private DialogueData currentDialogue;
    
    void Start()
    {
        if (dialogueJsonFile != null)
        {
            allDialogues = DialogueParser.ParseDialogues(dialogueJsonFile.text);
            
            currentDialogue = DialogueParser.GetStartDialogue(allDialogues);
            
            if (currentDialogue != null)
            {
                StartDialogue(currentDialogue);
            }
            
            foreach (var dialogue in allDialogues)
            {
                Debug.Log(dialogue.ToString());
            }
        }
    }
    
    void StartDialogue(DialogueData dialogue)
    {
        Debug.Log($"Начинаем диалог: {dialogue.Name}");
        
        foreach (string eventName in dialogue.OnStartEvents)
        {
            Debug.Log($"Событие начала: {eventName}");
        }
        
        ShowDialogueText(dialogue.Text);
    }
    
    void ShowDialogueText(string text)
    {
        Debug.Log($"Текст диалога: {text}");
    }
    
    public void GoToNextDialogue()
    {
        if (currentDialogue != null)
        {
            foreach (string eventName in currentDialogue.OnEndEvents)
            {
                Debug.Log($"Событие завершения: {eventName}");
            }
            
            if (currentDialogue.NextDialogue != null)
            {
                currentDialogue = currentDialogue.NextDialogue;
                StartDialogue(currentDialogue);
            }
            else
            {
                Debug.Log("Диалог завершен");
                EndDialogue();
            }
        }
    }
    
    void EndDialogue()
    {
        currentDialogue = null;
    }
}