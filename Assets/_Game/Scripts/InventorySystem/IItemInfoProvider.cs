namespace _Game.Scripts.InventorySystem
{
    public interface IItemInfoProvider
    {
        ItemConfig GetItemConfig(ItemId itemId);
    }
}