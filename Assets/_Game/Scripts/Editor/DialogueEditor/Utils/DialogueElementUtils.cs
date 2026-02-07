using System;
using Game.CustomEditorWindws.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Game.CustomEditorWindws.Utils
{
    public static class DialogueElementUtils
    {
        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };


            return button;
        }

        public static Port CreatePort(this DialogueNode node, string portName = "",
            Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

            port.portName = portName;
            
            return port;
        }
        
        public static Foldout CreateFoldout(string title,bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }
        
        
        public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField newTextField = new TextField()
            {
                value = value
            };

            if (onValueChanged != null)
            {
                newTextField.RegisterValueChangedCallback(onValueChanged);
            }
            
            return newTextField;
        }

        public static TextField CreateTextArea(string value = null,
            EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, onValueChanged);

            textArea.multiline = true;
            
            return textArea;
        }
    }
}