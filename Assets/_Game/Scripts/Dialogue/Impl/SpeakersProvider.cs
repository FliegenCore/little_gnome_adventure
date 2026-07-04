using System.Collections.Generic;
using _Game.Scripts.DialogueSystem.View;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem
{
    public class SpeakersProvider
    {
        private List<SpeakerView> _speakerViews;

        public SpeakersProvider()
        {
            _speakerViews = new List<SpeakerView>();
        }
        
        public SpeakerView GetSpeakerCharacter(string speakerName)
        {
            foreach (var speakerView in _speakerViews)
            {
                if (speakerView.Id.ToString() == speakerName)
                    return speakerView;
            }

            Debug.LogError($"{nameof(SpeakersProvider)} has not speaker by name {speakerName}");
            return null;
        }
        
        public void RegisterSpeakerCharacters(params SpeakerView[] speakerViews)
        {
            foreach (var speaker in speakerViews)
            {
                _speakerViews.Add(speaker);
            }
        }

        public void UnregisterSpeakerCharacters(List<SpeakerView> speakerViews)
        {
            foreach (var speaker in speakerViews)
            {
                _speakerViews.Remove(speaker);
            }
        }
        
        
    }
}