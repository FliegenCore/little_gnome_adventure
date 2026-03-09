using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem
{
    [Serializable]
    public class DialogueData
    {
        public string Id;                    
        public string Name;                  
        public string Text;                    
        public bool IsStart;                   
        public List<string> OnStartEvents;    
        public List<string> OnEndEvents;      
    
        public DialogueData NextDialogue;
    
        public override string ToString()
        {
            return $"Dialogue: {Name}, Text: {Text}, IsStart: {IsStart}, Next: {(NextDialogue != null ? NextDialogue.Name : "null")}";
        }
    }
    
    [Serializable]
    public class RawDialogueData
    {
        public List<RawNode> Nodes;
        public List<RawEdge> Edges;
    }

    [Serializable]
    public class RawNode
    {
        public string NodeId;
        public string DialogueName;
        public string Text;
        public int Type;
        public bool IsStartNode;
        public List<string> OnStartEvents;
        public List<string> OnEndEvents;
    }

    [Serializable]
    public class RawEdge
    {
        public string InputNodeId;
        public string OutputNodeId;
        public int InputPortIndex;
        public int OutputPortIndex;
    }
}