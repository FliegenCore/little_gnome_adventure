using System.Collections.Generic;
using System.Linq;
using Game.CustomEditorWindws.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.CustomEditorWindws.Elements
{
    public class DialogueNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DialogueType Type { get; set; }
        public bool IsStartNode { get; set; } 
        
        public List<string> OnStartEvents { get; set; }
        public List<string> OnEndEvents { get; set; }
        public Dictionary<int, List<string>> ChoiceEvents { get; set; }
        
        public Toggle startNodeToggle;

        public virtual void Initialize(Vector2 position)
        {
            DialogueName = "Dialogue name";
            Choices = new List<string>();
            Text = "Dialogue text.";
            SetPosition(new Rect(position, Vector2.zero));
            IsStartNode = false;
            
            OnStartEvents = new List<string>();
            OnEndEvents = new List<string>();
            ChoiceEvents = new Dictionary<int, List<string>>();
            
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public virtual void Draw()
        {
            TextField dialogueNameText = DialogueElementUtils.CreateTextField(DialogueName, evt =>
            {
                DialogueName = evt.newValue;
            });
            
            dialogueNameText.AddToClassList("ds-node__textfield");
            dialogueNameText.AddToClassList("ds-node__filename-textfield");
            dialogueNameText.AddToClassList("ds-node__textfield__hidden");
            
            VisualElement titleRow = new VisualElement();
            titleRow.style.flexDirection = FlexDirection.Row;
            titleRow.style.alignItems = Align.Center;
            titleRow.style.justifyContent = Justify.SpaceBetween;
            
            startNodeToggle = new Toggle("Start");
            startNodeToggle.value = IsStartNode;
            startNodeToggle.RegisterValueChangedCallback(evt =>
            {
                IsStartNode = evt.newValue;
                UpdateStartNodeToggleStyle();
            });
            
            startNodeToggle.AddToClassList("ds-node__start-toggle");
            UpdateStartNodeToggleStyle();
            
            titleRow.Add(dialogueNameText);
            titleRow.Add(startNodeToggle);
            
            titleContainer.Clear();
            titleContainer.Add(titleRow);
            
            Port inputPort = this.CreatePort("Dialogue connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Dialogue connection";
            inputContainer.Add(inputPort);
            
            VisualElement customDataContainer = new VisualElement();
            customDataContainer.AddToClassList("ds-node__custom-data-container");

            Foldout eventsFoldout = DialogueElementUtils.CreateFoldout("Events");
            
            Foldout startEventsFoldout = DialogueElementUtils.CreateFoldout("On Start Events");
            UpdateEventsList(startEventsFoldout, OnStartEvents, events => OnStartEvents = events);
            eventsFoldout.Add(startEventsFoldout);
            
            Foldout endEventsFoldout = DialogueElementUtils.CreateFoldout("On End Events");
            UpdateEventsList(endEventsFoldout, OnEndEvents, events => OnEndEvents = events);
            eventsFoldout.Add(endEventsFoldout);
            
            customDataContainer.Add(eventsFoldout);

            Foldout textFoldout = DialogueElementUtils.CreateFoldout("Dialogue text");
            TextField textTextField = DialogueElementUtils.CreateTextArea(Text, evt =>
            {
                Text = evt.newValue;
            });
            
            textTextField.AddToClassList("ds-node__textfield");
            textTextField.AddToClassList("ds-node__quote-textfield");
            
            textFoldout.Add(textTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
        }

        private void UpdateStartNodeToggleStyle()
        {
            if (IsStartNode)
            {
                startNodeToggle.AddToClassList("ds-node__start-toggle-active");
                startNodeToggle.RemoveFromClassList("ds-node__start-toggle-inactive");
            }
            else
            {
                startNodeToggle.AddToClassList("ds-node__start-toggle-inactive");
                startNodeToggle.RemoveFromClassList("ds-node__start-toggle-active");
            }
        }
        
        private void UpdateEventsList(Foldout foldout, List<string> eventsList, System.Action<List<string>> onListUpdated)
        {
            foldout.Clear();
            
            VisualElement eventsContainer = new VisualElement();
            eventsContainer.AddToClassList("events-container");
            
            for (int i = 0; i < eventsList.Count; i++)
            {
                int index = i;
                string eventValue = eventsList[i];
                
                VisualElement eventRow = CreateEventRow(eventValue, () =>
                {
                    eventsList.RemoveAt(index);
                    UpdateEventsList(foldout, eventsList, onListUpdated);
                    onListUpdated?.Invoke(eventsList);
                });
                
                TextField eventField = eventRow.Q<TextField>();
                if (eventField != null)
                {
                    eventField.RegisterValueChangedCallback(evt =>
                    {
                        if (index < eventsList.Count)
                        {
                            eventsList[index] = evt.newValue;
                            onListUpdated?.Invoke(eventsList);
                        }
                    });
                }
                
                eventsContainer.Add(eventRow);
            }
            
            Button addEventButton = DialogueElementUtils.CreateButton("Add Event", () =>
            {
                eventsList.Add("Event_ID");
                UpdateEventsList(foldout, eventsList, onListUpdated);
                onListUpdated?.Invoke(eventsList);
            });
            addEventButton.AddToClassList("ds-node__button");
            
            foldout.Add(eventsContainer);
            foldout.Add(addEventButton);
        }
        
        private VisualElement CreateEventRow(string eventValue, System.Action onRemove)
        {
            VisualElement row = new VisualElement();
            row.AddToClassList("event-row");
            row.style.flexDirection = FlexDirection.Row;
            row.style.marginBottom = 5;
            
            TextField eventField = DialogueElementUtils.CreateTextField(eventValue);
            eventField.AddToClassList("ds-node__textfield");
            eventField.AddToClassList("event-textfield");
            eventField.style.flexGrow = 1;
            eventField.style.marginRight = 5;
            
            Button removeButton = DialogueElementUtils.CreateButton("X", onRemove);
            removeButton.AddToClassList("ds-node__button");
            removeButton.AddToClassList("remove-button");
            
            row.Add(eventField);
            row.Add(removeButton);
            
            return row;
        }
    }
}