using System;
using System.Collections.Generic;
using System.IO;
using Game.CustomEditorWindws.Elements;
using UnityEngine;

namespace Game.CustomEditorWindws.Data
{
    [System.Serializable]
    public class SerializableNode
    {
        public string NodeId;
        public string DialogueName;
        public string Text;
        public DialogueType Type;
        public Vector2 Position;
        public bool IsStartNode;
    
        public List<string> OnStartEvents = new List<string>();
        public List<string> OnEndEvents = new List<string>();
        public List<ChoiceData> ChoiceEvents = new List<ChoiceData>();
        public List<string> Choices = new List<string>();
    }

    [System.Serializable]
    public class ChoiceData
    {
        public int ChoiceIndex;
        public List<string> Events;
    }

    [System.Serializable]
    public class SerializableEdge
    {
        public string InputNodeId;
        public string OutputNodeId;
        public int InputPortIndex;
        public int OutputPortIndex;
    }

    [System.Serializable]
    public class DialogueGraphData
    {
        public List<SerializableNode> Nodes = new List<SerializableNode>();
        public List<SerializableEdge> Edges = new List<SerializableEdge>();
    }
}