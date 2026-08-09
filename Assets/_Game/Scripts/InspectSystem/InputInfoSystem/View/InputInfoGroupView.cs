using TMPro;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.InputInfoSystem
{
    public class InputInfoGroupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;
        
        public void SetDescription(string description)
        {
            _descriptionText.text = $"- {description}";
        }
    }
}