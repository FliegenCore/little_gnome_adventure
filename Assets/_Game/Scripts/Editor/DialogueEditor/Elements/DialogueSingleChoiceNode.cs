using Game.CustomEditorWindws.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.CustomEditorWindws.Elements
{
    public class DialogueSingleChoiceNode : DialogueNode
    {
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            Type = DialogueType.SingleChoice;
            
            Choices.Add("Next dialogue");
        }

        public override void Draw()
        {
            base.Draw();

            foreach (var choice in Choices)
            {
                Port choicePort = this.CreatePort(choice);
                choicePort.portName = choice;
                outputContainer.Add(choicePort);
            }
            
            RefreshExpandedState();
            
        }
    }
}