using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.DialogueSystem;
using UnityEngine;

public static class DialogueParser
{
    public static List<DialogueData> ParseDialogues(string json)
    {
        RawDialogueData rawData = JsonUtility.FromJson<RawDialogueData>(json);
        
        Dictionary<string, DialogueData> dialogueDict = new Dictionary<string, DialogueData>();
        
        foreach (var rawNode in rawData.Nodes)
        {
            var dialogue = new DialogueData
            {
                Id = rawNode.NodeId,
                Name = rawNode.DialogueName,
                Text = rawNode.Text,
                IsStart = rawNode.IsStartNode,
                OnStartEvents = rawNode.OnStartEvents ?? new List<string>(),
                OnEndEvents = rawNode.OnEndEvents ?? new List<string>()
            };
            
            dialogueDict[rawNode.NodeId] = dialogue;
        }
        
        foreach (var edge in rawData.Edges)
        {
            if (dialogueDict.ContainsKey(edge.OutputNodeId) && 
                dialogueDict.ContainsKey(edge.InputNodeId))
            {
                var fromDialogue = dialogueDict[edge.OutputNodeId];
                var toDialogue = dialogueDict[edge.InputNodeId];
                
                fromDialogue.NextDialogue = toDialogue;
            }
        }
        
        return dialogueDict.Values.ToList();
    }
    
    public static DialogueData GetStartDialogue(List<DialogueData> dialogues)
    {
        return dialogues.FirstOrDefault(d => d.IsStart);
    }
    
    public static DialogueData GetDialogueByName(List<DialogueData> dialogues, string name)
    {
        return dialogues.FirstOrDefault(d => d.Name == name);
    }
}