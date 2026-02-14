using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items.Hints
{
    public class EyeHintSelect : AbstractHintSelect
    {
        [SerializeField] private GameObject _eyeHint;

        protected override void HintSelect(bool isSelect)
        {
            _eyeHint.gameObject.SetActive(isSelect);
        }
    }
}