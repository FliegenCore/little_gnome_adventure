using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Sound.Editor
{
    [CustomEditor(typeof(AudioStorageConfig))]
    public class CustomEditorButton : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AudioStorageConfig storageConfig = (AudioStorageConfig)target;

            if (GUILayout.Button("LoadAll"))
            {
                storageConfig.LoadAllAudioClips();
            }
        }
    }
}