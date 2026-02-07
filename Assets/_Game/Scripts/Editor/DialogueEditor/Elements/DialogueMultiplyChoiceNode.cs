using System.Collections.Generic;
using Game.CustomEditorWindws.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.CustomEditorWindws.Elements
{
    public class DialogueMultiplyChoiceNode : DialogueNode
    {
        private Dictionary<int, VisualElement> choiceContainers = new Dictionary<int, VisualElement>();
        private Dictionary<int, Button> toggleButtons = new Dictionary<int, Button>();
        private List<Port> choicePorts = new List<Port>();
        private List<VisualElement> eventPopups = new List<VisualElement>();

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            Type = DialogueType.MultipleChoices;
            
            if (Choices.Count == 0)
            {
                Choices.Add("Choice 1");
            }
            
            for (int i = 0; i < Choices.Count; i++)
            {
                if (!ChoiceEvents.ContainsKey(i))
                {
                    ChoiceEvents[i] = new List<string>();
                }
            }
        }

        public override void Draw()
        {
            base.Draw();

            AddToClassList("multiply-choice-node");

            Button addChoiceButton = DialogueElementUtils.CreateButton("Add Choice", () =>
            {
                int newIndex = Choices.Count;
                Choices.Add($"Choice {newIndex + 1}");
                ChoiceEvents[newIndex] = new List<string>();
                RedrawChoices();
            });
            
            addChoiceButton.AddToClassList("ds-node__button");
            addChoiceButton.AddToClassList("add-choice-button");
            addChoiceButton.style.marginTop = 10;
            addChoiceButton.style.marginBottom = 10;
            
            mainContainer.Insert(1, addChoiceButton);
            
            RedrawChoices();
        }

        private void RedrawChoices()
        {
            // Очищаем старые порты
            foreach (var port in choicePorts)
            {
                outputContainer.Remove(port);
            }
            choicePorts.Clear();
            
            // Очищаем старые попапы
            foreach (var popup in eventPopups)
            {
                if (popup.parent == this)
                {
                    Remove(popup);
                }
            }
            eventPopups.Clear();
            choiceContainers.Clear();
            toggleButtons.Clear();
            
            // Очищаем output контейнер
            outputContainer.Clear();
            
            // Создаем новые порты и попапы
            for (int i = 0; i < Choices.Count; i++)
            {
                Port choicePort = CreateChoicePort(i);
                outputContainer.Add(choicePort);
                choicePorts.Add(choicePort);
                
                // Создаем попап для событий
                VisualElement eventsPopup = CreateEventsPopup(i);
                Add(eventsPopup);
                eventPopups.Add(eventsPopup);
                choiceContainers[i] = eventsPopup;
            }
            
            RefreshExpandedState();
        }

        private Port CreateChoicePort(int choiceIndex)
        {
            Port choicePort = this.CreatePort();
            choicePort.portName = "";
            choicePort.AddToClassList("choice-port");
            
            // Контейнер для всего выбора
            VisualElement choiceContainer = new VisualElement();
            choiceContainer.AddToClassList("choice-container");
            
            // Верхний ряд с названием и кнопкой удаления
            VisualElement choiceRow = CreateChoiceRow(choiceIndex);
            choiceContainer.Add(choiceRow);
            
            // Кнопка событий - отдельный ряд
            Button toggleButton = CreateToggleButton(choiceIndex);
            choiceContainer.Add(toggleButton);
            
            toggleButtons[choiceIndex] = toggleButton;
            
            choicePort.Add(choiceContainer);
            
            return choicePort;
        }

        private VisualElement CreateChoiceRow(int choiceIndex)
        {
            VisualElement row = new VisualElement();
            row.AddToClassList("choice-row");
            row.style.flexDirection = FlexDirection.Row;
            row.style.alignItems = Align.Center;
            row.style.marginBottom = 5;
            
            // Поле для названия выбора
            TextField choiceTextField = DialogueElementUtils.CreateTextField(
                Choices[choiceIndex],
                evt =>
                {
                    if (choiceIndex < Choices.Count)
                    {
                        Choices[choiceIndex] = evt.newValue;
                    }
                }
            );
            
            choiceTextField.AddToClassList("ds-node__textfield");
            choiceTextField.AddToClassList("ds-node__choice-textfield");
            choiceTextField.style.flexGrow = 1;
            choiceTextField.style.marginRight = 10;
            choiceTextField.style.minWidth = 180;
            choiceTextField.style.maxWidth = 200;
            
            // Кнопка удаления
            if (Choices.Count > 1)
            {
                Button deleteButton = DialogueElementUtils.CreateButton("X", () =>
                {
                    RemoveChoice(choiceIndex);
                });
                
                deleteButton.AddToClassList("ds-node__button");
                deleteButton.AddToClassList("ds-node__button-danger");
                deleteButton.style.width = 25;
                deleteButton.style.height = 20;
                deleteButton.style.flexShrink = 0;
                
                row.Add(choiceTextField);
                row.Add(deleteButton);
            }
            else
            {
                row.Add(choiceTextField);
                
                // Заглушка для выравнивания
                VisualElement spacer = new VisualElement();
                spacer.style.width = 25;
                row.Add(spacer);
            }
            
            return row;
        }

        private Button CreateToggleButton(int choiceIndex)
        {
            Button toggleButton = DialogueElementUtils.CreateButton("Events ▼", () =>
            {
                ToggleEventsVisibility(choiceIndex);
            });
            
            toggleButton.AddToClassList("ds-node__button");
            toggleButton.AddToClassList("toggle-events-button");
            toggleButton.style.alignSelf = Align.FlexStart;
            toggleButton.style.marginTop = 0;
            toggleButton.style.marginBottom = 5;
            
            return toggleButton;
        }

        private VisualElement CreateEventsPopup(int choiceIndex)
        {
            VisualElement popup = new VisualElement();
            popup.AddToClassList("events-popup");
            popup.style.display = DisplayStyle.None;
            popup.style.position = Position.Absolute;
            
            VisualElement popupContent = new VisualElement();
            popupContent.AddToClassList("events-popup-content");
            
            // Заголовок
            Label titleLabel = new Label($"Events for Choice {choiceIndex + 1}");
            titleLabel.AddToClassList("events-popup-title");
            popupContent.Add(titleLabel);
            
            // Список событий
            VisualElement eventsListContainer = new VisualElement();
            eventsListContainer.AddToClassList("events-list-container");
            UpdateEventsList(eventsListContainer, choiceIndex);
            popupContent.Add(eventsListContainer);
            
            // Кнопка добавления
            Button addButton = DialogueElementUtils.CreateButton("+ Add Event", () =>
            {
                AddEventToChoice(choiceIndex);
            });
            
            addButton.AddToClassList("ds-node__button");
            addButton.AddToClassList("add-event-button");
            addButton.style.marginTop = 10;
            
            popupContent.Add(addButton);
            
            popup.Add(popupContent);
            
            return popup;
        }

        private void UpdateEventsList(VisualElement container, int choiceIndex)
        {
            container.Clear();
            
            if (!ChoiceEvents.ContainsKey(choiceIndex))
            {
                ChoiceEvents[choiceIndex] = new List<string>();
            }
            
            var events = ChoiceEvents[choiceIndex];
            
            for (int i = 0; i < events.Count; i++)
            {
                int eventIndex = i;
                VisualElement eventRow = CreateEventRow(choiceIndex, eventIndex, events[i]);
                container.Add(eventRow);
            }
        }

        private VisualElement CreateEventRow(int choiceIndex, int eventIndex, string eventValue)
        {
            VisualElement eventRow = new VisualElement();
            eventRow.AddToClassList("event-row");
            eventRow.style.flexDirection = FlexDirection.Row;
            eventRow.style.alignItems = Align.Center;
            eventRow.style.marginBottom = 5;
            
            TextField eventField = DialogueElementUtils.CreateTextField(
                eventValue,
                evt =>
                {
                    if (choiceIndex < Choices.Count && 
                        ChoiceEvents.ContainsKey(choiceIndex) && 
                        eventIndex < ChoiceEvents[choiceIndex].Count)
                    {
                        ChoiceEvents[choiceIndex][eventIndex] = evt.newValue;
                    }
                }
            );
            
            eventField.AddToClassList("ds-node__textfield");
            eventField.AddToClassList("event-textfield");
            eventField.style.flexGrow = 1;
            eventField.style.marginRight = 5;
            eventField.style.minWidth = 150;
            
            Button removeButton = DialogueElementUtils.CreateButton("X", () =>
            {
                RemoveEventFromChoice(choiceIndex, eventIndex);
            });
            
            removeButton.AddToClassList("ds-node__button");
            removeButton.AddToClassList("remove-button");
            removeButton.style.width = 25;
            removeButton.style.height = 20;
            removeButton.style.flexShrink = 0;
            
            eventRow.Add(eventField);
            eventRow.Add(removeButton);
            
            return eventRow;
        }

        private void ToggleEventsVisibility(int choiceIndex)
        {
            // Скрываем все другие попапы
            foreach (var popup in eventPopups)
            {
                if (popup != choiceContainers[choiceIndex] && popup.style.display != DisplayStyle.None)
                {
                    popup.style.display = DisplayStyle.None;
                }
            }
            
            if (choiceContainers.TryGetValue(choiceIndex, out var container) &&
                toggleButtons.TryGetValue(choiceIndex, out var button))
            {
                if (container.style.display == DisplayStyle.None)
                {
                    // Показываем попап
                    container.style.display = DisplayStyle.Flex;
                    button.text = "Events ▲";
                    
                    // Позиционируем попап относительно кнопки
                    var buttonWorldPos = button.worldBound;
                    var nodeWorldPos = this.worldBound;
                    
                    float localX = buttonWorldPos.x - nodeWorldPos.x;
                    float localY = buttonWorldPos.y - nodeWorldPos.y + buttonWorldPos.height + 5;
                    
                    container.style.left = localX;
                    container.style.top = localY;
                    
                    container.BringToFront();
                }
                else
                {
                    container.style.display = DisplayStyle.None;
                    button.text = "Events ▼";
                }
            }
        }

        private void AddEventToChoice(int choiceIndex)
        {
            if (!ChoiceEvents.ContainsKey(choiceIndex))
            {
                ChoiceEvents[choiceIndex] = new List<string>();
            }
            
            ChoiceEvents[choiceIndex].Add("New Event");
            
            if (choiceContainers.TryGetValue(choiceIndex, out var container))
            {
                var listContainer = container.Q(null, "events-list-container");
                if (listContainer != null)
                {
                    UpdateEventsList(listContainer, choiceIndex);
                }
            }
        }

        private void RemoveEventFromChoice(int choiceIndex, int eventIndex)
        {
            if (ChoiceEvents.ContainsKey(choiceIndex) && 
                eventIndex >= 0 && 
                eventIndex < ChoiceEvents[choiceIndex].Count)
            {
                ChoiceEvents[choiceIndex].RemoveAt(eventIndex);
                
                if (choiceContainers.TryGetValue(choiceIndex, out var container))
                {
                    var listContainer = container.Q(null, "events-list-container");
                    if (listContainer != null)
                    {
                        UpdateEventsList(listContainer, choiceIndex);
                    }
                }
            }
        }

        private void RemoveChoice(int choiceIndex)
        {
            if (Choices.Count <= 1) return;
            
            if (choiceIndex >= 0 && choiceIndex < Choices.Count)
            {
                if (choiceContainers.TryGetValue(choiceIndex, out var container))
                {
                    eventPopups.Remove(container);
                    if (container.parent == this)
                    {
                        Remove(container);
                    }
                    choiceContainers.Remove(choiceIndex);
                }
                
                toggleButtons.Remove(choiceIndex);
                Choices.RemoveAt(choiceIndex);
                
                var newEvents = new Dictionary<int, List<string>>();
                foreach (var kvp in ChoiceEvents)
                {
                    if (kvp.Key < choiceIndex)
                    {
                        newEvents[kvp.Key] = kvp.Value;
                    }
                    else if (kvp.Key > choiceIndex)
                    {
                        newEvents[kvp.Key - 1] = kvp.Value;
                    }
                }
                
                ChoiceEvents = newEvents;
                
                RedrawChoices();
            }
        }
    }
}