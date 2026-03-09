using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueProvider
    {
        private const string PATH = "DialoguesJson";
        
        private Dictionary<string, TextAsset> _dialogueJsons = new();

        public DialogueProvider()
        {
            LoadAllDialogues();
        }
        
        private void LoadAllDialogues()
        {
            TextAsset[] resourcesDialogues = Resources.LoadAll<TextAsset>(PATH);
        
            if (resourcesDialogues.Length > 0)
            {
                foreach (TextAsset dialogueFile in resourcesDialogues)
                {
                    string key = dialogueFile.name;
                    _dialogueJsons[key] = dialogueFile;
                
                    Debug.Log($"Загружен диалог из Resources: {key}");
                }
            
                Debug.Log($"Всего загружено диалогов из Resources: {_dialogueJsons.Count}");
            }
        }

        public List<DialogueData> GetDialogue(string dialogueName)
        {
            if (_dialogueJsons.TryGetValue(dialogueName, out TextAsset dialogue))
            {
                return DialogueParser.ParseDialogues(dialogue.text);
            }
            
            Debug.LogError($"{nameof(DialogueProvider)} has not dialogue named {dialogueName}");
            return null;
        }

        public DialogueData GetStartDialogueData(List<DialogueData> dialogueDatas)
        {
            return DialogueParser.GetStartDialogue(dialogueDatas);
        }
    }
}