using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.CustomEditorWindws.Data;
using Game.CustomEditorWindws.Elements;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.CustomEditorWindws
{
    public class DialogueGraphView : GraphView
    {
        private Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();
        private DialogueNode startNode;
        
        public DialogueGraphView()
        {
            AddBackground();
            AddManipulators();
            AddStyles();
            AddToolbar();
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();
            
            Button saveButton = new Button(SaveGraph)
            {
                text = "Save Graph"
            };
            
            Button loadButton = new Button(LoadGraph)
            {
                text = "Load Graph"
            };
            
            toolbar.Add(saveButton);
            toolbar.Add(loadButton);
            
            Add(toolbar);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port)
                {
                    return;
                }

                if (startPort.node == port.node)
                {
                    return;
                }

                if (startPort.direction == port.direction)
                {
                    return;
                }
                
                compatiblePorts.Add(port);
            });
            
            return compatiblePorts;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            this.AddManipulator(CreateNodeContextualMenu("Add node (Single choice)", DialogueType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add node (Multiply choice)", DialogueType.MultipleChoices));
        }

        private IManipulator CreateNodeContextualMenu(string actionTitle, DialogueType dialogueType)
        {
            ContextualMenuManipulator contextualMenuManager = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(CreateNode(dialogueType, actionEvent.eventInfo.localMousePosition)))
            );
            
            return contextualMenuManager;
        }

        private void AddStyles()
        {
            StyleSheet styleGraphSheet = (StyleSheet)EditorGUIUtility.Load("DialogueEditor/DialogueGraphViewStyles.uss");
            StyleSheet styleNodeSheet = (StyleSheet)EditorGUIUtility.Load("DialogueEditor/DialogueNodeStyles.uss");
            
            styleSheets.Add(styleGraphSheet);
            styleSheets.Add(styleNodeSheet);
        }

        private void AddBackground()
        {
            GridBackground gridBackground = new GridBackground();
            
            gridBackground.StretchToParentSize();
            
            Insert(0, gridBackground);
        }

        private DialogueNode CreateNode(DialogueType dialogueType, Vector2 position, bool needDraw = true)
        {
            DialogueNode node = null;
            
            switch (dialogueType)
            {
                case DialogueType.SingleChoice:
                    node = new DialogueSingleChoiceNode();
                    break;
                case DialogueType.MultipleChoices:
                    node = new DialogueMultiplyChoiceNode();
                    break;
                default:
                    node = new DialogueSingleChoiceNode();
                    break;
            }
            
            node.Initialize(position);
            if(needDraw)
                node.Draw();
            
            node.RegisterCallback<MouseDownEvent>(evt =>
            {
                if (evt.clickCount == 2)
                {
                    ToggleStartNode(node);
                }
            });
            
            nodeLookup[node.viewDataKey] = node;
            return node;
        }
        
        private void ToggleStartNode(DialogueNode node)
        {
            if (startNode != null && startNode != node)
            {
                startNode.IsStartNode = false;
                startNode.startNodeToggle.value = false;
            }
            
            node.IsStartNode = !node.IsStartNode;
            node.startNodeToggle.value = node.IsStartNode;
            
            if (node.IsStartNode)
            {
                startNode = node;
            }
            else
            {
                startNode = null;
            }
        }

        private void SaveGraph()
{
    DialogueGraphData graphData = new DialogueGraphData();
    
    foreach (var node in nodes.OfType<DialogueNode>())
    {
        SerializableNode serializableNode = new SerializableNode
        {
            NodeId = node.viewDataKey,
            DialogueName = node.DialogueName,
            Text = node.Text,
            Type = node.Type,
            Position = node.GetPosition().position,
            IsStartNode = node.IsStartNode,
            OnStartEvents = node.OnStartEvents?.ToList() ?? new List<string>(),
            OnEndEvents = node.OnEndEvents?.ToList() ?? new List<string>(),
            Choices = node.Choices?.ToList() ?? new List<string>(),
            ChoiceEvents = new List<ChoiceData>()
        };
        
        if (node.ChoiceEvents != null)
        {
            foreach (var kvp in node.ChoiceEvents)
            {
                serializableNode.ChoiceEvents.Add(new ChoiceData
                {
                    ChoiceIndex = kvp.Key,
                    Events = kvp.Value?.ToList() ?? new List<string>()
                });
            }
        }
        
        graphData.Nodes.Add(serializableNode);
    }
    
    foreach (var edge in edges)
    {
        if (edge.input.node is DialogueNode inputNode && edge.output.node is DialogueNode outputNode)
        {
            int inputIndex = -1;
            int outputIndex = -1;
            
            for (int i = 0; i < inputNode.inputContainer.childCount; i++)
            {
                if (inputNode.inputContainer[i] == edge.input)
                {
                    inputIndex = i;
                    break;
                }
            }
            
            for (int i = 0; i < outputNode.outputContainer.childCount; i++)
            {
                if (outputNode.outputContainer[i] == edge.output)
                {
                    outputIndex = i;
                    break;
                }
            }
            
            if (inputIndex >= 0 && outputIndex >= 0)
            {
                SerializableEdge serializableEdge = new SerializableEdge
                {
                    InputNodeId = inputNode.viewDataKey,
                    OutputNodeId = outputNode.viewDataKey,
                    InputPortIndex = inputIndex,
                    OutputPortIndex = outputIndex
                };
                
                graphData.Edges.Add(serializableEdge);
            }
        }
    }
    
    string json = JsonConvert.SerializeObject(graphData, Formatting.Indented, 
        new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Include
        });
    
    string path = EditorUtility.SaveFilePanel("Save Dialogue Graph", Application.dataPath, "dialogue_graph", "json");
    if (!string.IsNullOrEmpty(path))
    {
        File.WriteAllText(path, json);
        Debug.Log($"Graph saved to: {path}");
    }
}

        private void LoadGraph()
        {
            string path = EditorUtility.OpenFilePanel("Load Dialogue Graph", Application.dataPath, "json");
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }
            
            try
            {
                string json = File.ReadAllText(path);
                DialogueGraphData graphData = JsonConvert.DeserializeObject<DialogueGraphData>(json);
                
                ClearGraph();
                LoadNodes(graphData);
                LoadEdges(graphData);
                
                Debug.Log($"Graph loaded from: {path}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load graph: {e.Message}");
            }
        }

        private void ClearGraph()
        {
            foreach (var node in nodes.ToList())
            {
                RemoveElement(node);
            }
            
            nodeLookup.Clear();
            startNode = null;
        }

        private void LoadNodes(DialogueGraphData graphData)
        {
            foreach (var serializableNode in graphData.Nodes)
            {
                DialogueNode node = CreateNode(serializableNode.Type, serializableNode.Position, false);
                node.viewDataKey = serializableNode.NodeId;
                node.DialogueName = serializableNode.DialogueName;
                node.Text = serializableNode.Text;
                node.IsStartNode = serializableNode.IsStartNode;
                node.OnStartEvents = serializableNode.OnStartEvents?.ToList() ?? new List<string>();
                node.OnEndEvents = serializableNode.OnEndEvents?.ToList() ?? new List<string>();
                node.Choices = serializableNode.Choices?.ToList() ?? new List<string>();
                
                node.ChoiceEvents = new Dictionary<int, List<string>>();
                if (serializableNode.ChoiceEvents != null)
                {
                    foreach (var choiceData in serializableNode.ChoiceEvents)
                    {
                        node.ChoiceEvents[choiceData.ChoiceIndex] = choiceData.Events?.ToList() ?? new List<string>();
                    }
                }
                
                AddElement(node);
                nodeLookup[serializableNode.NodeId] = node;
                
                if (serializableNode.IsStartNode)
                {
                    startNode = node;
                }
                
                node.Draw();
            }
        }

        private void LoadEdges(DialogueGraphData graphData)
        {
            foreach (var serializableEdge in graphData.Edges)
            {
                if (nodeLookup.TryGetValue(serializableEdge.InputNodeId, out var inputNode) &&
                    nodeLookup.TryGetValue(serializableEdge.OutputNodeId, out var outputNode))
                {
                    Port inputPort = null;
                    Port outputPort = null;
                    
                    if (serializableEdge.InputPortIndex >= 0 && serializableEdge.InputPortIndex < inputNode.inputContainer.childCount)
                    {
                        inputPort = inputNode.inputContainer[serializableEdge.InputPortIndex] as Port;
                    }
                    
                    if (serializableEdge.OutputPortIndex >= 0 && serializableEdge.OutputPortIndex < outputNode.outputContainer.childCount)
                    {
                        outputPort = outputNode.outputContainer[serializableEdge.OutputPortIndex] as Port;
                    }
                    
                    if (inputPort != null && outputPort != null)
                    {
                        Edge edge = new Edge
                        {
                            input = inputPort,
                            output = outputPort
                        };
                        
                        inputPort.Connect(edge);
                        outputPort.Connect(edge);
                        AddElement(edge);
                    }
                }
            }
        }
    }
}