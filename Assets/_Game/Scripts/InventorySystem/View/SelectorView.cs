using UniRx;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class SelectorView : MonoBehaviour
    {
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}