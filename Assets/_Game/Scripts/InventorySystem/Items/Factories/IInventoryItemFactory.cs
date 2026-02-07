namespace _Game.Scripts.InventorySystem.Factories
{
    public interface IInventoryItemFactory
    {
        InventoryItem CreateItem(ItemId id);
    }
}