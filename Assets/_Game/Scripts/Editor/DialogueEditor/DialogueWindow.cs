using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;
using System.Linq;

namespace Game.CustomEditorWindws
{
    public class DialogueWindow : EditorWindow
    {
        private DialogueGraphView graphView;
        private string currentFilePath;
        private bool isReloading = false;

        [MenuItem("Hell/DialogueWindow")]
        public static void ShowWindow()
        {
            GetWindow<DialogueWindow>("Dialogue Window");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddStyles();
            RegisterDragAndDrop();
            RegisterKeyboardShortcuts();
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            // Подписываемся на событие завершения перекомпиляции скриптов
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
            
            UpdateTitle();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
        }

        private void AddGraphView()
        {
            graphView = new DialogueGraphView();
            graphView.parentWindow = this;
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("DialogueEditor/DialogueVariables.uss");
            if (styleSheet != null)
                rootVisualElement.styleSheets.Add(styleSheet);
        }

        private void RegisterDragAndDrop()
        {
            rootVisualElement.RegisterCallback<DragUpdatedEvent>(OnDragUpdated);
            rootVisualElement.RegisterCallback<DragPerformEvent>(OnDragPerform);
            rootVisualElement.RegisterCallback<DragExitedEvent>(OnDragExited);
        }

        private void RegisterKeyboardShortcuts()
        {
            rootVisualElement.RegisterCallback<KeyDownEvent>(OnKeyDown);
            rootVisualElement.focusable = true;
            rootVisualElement.Focus();
        }

        private void OnDragUpdated(DragUpdatedEvent evt)
        {
            bool isValid = false;
            foreach (var obj in DragAndDrop.objectReferences)
            {
                if (obj is TextAsset)
                {
                    string path = AssetDatabase.GetAssetPath(obj);
                    if (path.EndsWith(".json", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            DragAndDrop.visualMode = isValid ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;
        }

        private void OnDragPerform(DragPerformEvent evt)
        {
            foreach (var obj in DragAndDrop.objectReferences)
            {
                if (obj is TextAsset textAsset)
                {
                    string path = AssetDatabase.GetAssetPath(textAsset);
                    if (File.Exists(path) && path.EndsWith(".json", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        LoadGraph(path);
                        DragAndDrop.AcceptDrag();
                        evt.StopPropagation();
                        break;
                    }
                }
            }
        }

        private void OnDragExited(DragExitedEvent evt)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.None;
        }

        private void OnKeyDown(KeyDownEvent evt)
        {
            if ((evt.ctrlKey || evt.commandKey) && evt.keyCode == KeyCode.S)
            {
                SaveCurrentGraph();
                evt.StopPropagation();
            }
        }

        private void SaveCurrentGraph()
        {
            // Проверка: существует ли граф и есть ли в нём узлы
            if (graphView == null)
            {
                Debug.LogWarning("GraphView is null, cannot save.");
                EditorUtility.DisplayDialog("Warning", "GraphView is not available. Try reopening the window.", "OK");
                return;
            }
            
            if (!graphView.nodes.Any())
            {
                EditorUtility.DisplayDialog("Warning", "Cannot save an empty graph. Add at least one node before saving.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(currentFilePath))
            {
                string newPath = EditorUtility.SaveFilePanel("Save Dialogue Graph", Application.dataPath, "dialogue_graph", "json");
                if (string.IsNullOrEmpty(newPath))
                    return;
                currentFilePath = newPath;
            }
            graphView.SaveGraph(currentFilePath);
            UpdateTitle();
            Debug.Log($"Graph saved to: {currentFilePath}");
        }

        private void LoadGraph(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"File not found: {path}");
                return;
            }

            if (graphView == null)
            {
                Debug.LogError("GraphView is null, cannot load.");
                return;
            }

            graphView.ClearGraph();
            graphView.LoadGraph(path);
            currentFilePath = path;
            UpdateTitle();
        }

        public void UpdateTitle()
        {
            string fileName = string.IsNullOrEmpty(currentFilePath) ? "Untitled" : Path.GetFileName(currentFilePath);
            titleContent.text = $"Dialogue Window - {fileName}";
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                // При выходе из Edit Mode в Play Mode сбрасываем путь,
                // чтобы не сохранить пустой граф после возврата
                // Но запоминаем последний путь в отдельной переменной для возможного восстановления
                if (!string.IsNullOrEmpty(currentFilePath) && File.Exists(currentFilePath))
                {
                    // Сохраняем последний путь в EditorPrefs для восстановления после перезагрузки
                    EditorPrefs.SetString("DialogueWindow_LastPath", currentFilePath);
                }
                currentFilePath = null;
                UpdateTitle();
            }
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                // При возврате в Edit Mode пытаемся восстановить последний файл
                TryRestoreLastFile();
            }
        }

        private void OnAfterAssemblyReload()
        {
            // После перекомпиляции скриптов пытаемся восстановить последний файл
            TryRestoreLastFile();
        }

        private void TryRestoreLastFile()
        {
            if (isReloading) return;
            isReloading = true;
            
            // Небольшая задержка, чтобы граф успел создаться
            EditorApplication.delayCall += () =>
            {
                if (graphView == null || !graphView.nodes.Any())
                {
                    string lastPath = EditorPrefs.GetString("DialogueWindow_LastPath", "");
                    if (!string.IsNullOrEmpty(lastPath) && File.Exists(lastPath))
                    {
                        Debug.Log($"Restoring last opened file: {lastPath}");
                        LoadGraph(lastPath);
                    }
                    else
                    {
                        currentFilePath = null;
                        UpdateTitle();
                    }
                }
                isReloading = false;
            };
        }
    }
}