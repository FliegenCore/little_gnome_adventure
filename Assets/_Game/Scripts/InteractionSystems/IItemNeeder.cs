using System;

namespace _Game.Scripts.InventorySystem
{
    public interface IItemNeeder
    {
        void InteractWithItem(InventoryItem item, Action callback);
    }
}