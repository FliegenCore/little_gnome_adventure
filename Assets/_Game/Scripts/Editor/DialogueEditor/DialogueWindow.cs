using UnityEditor;
using UnityEngine.UIElements;

namespace Game.CustomEditorWindws
{
    public class DialogueWindow : EditorWindow
    {
        [MenuItem("Hell/DialogueWindow")]
        public static void ShowWindow()
        {
            GetWindow<DialogueWindow>("Dialogue Window");
        }

        private void OnEnable()
        {
            AddGraphView();

            AddStyles();
        }

        private void AddGraphView()
        {
            DialogueGraphView graphView = new DialogueGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }
        
        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("DialogueEditor/DialogueVariables.uss");
            
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}
