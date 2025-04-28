using System;

public static class ItemService
{
    public static Action<ItemData> OnItemEquipped;
    public static Action<ItemData> OnItemUnequipped;
    public static Action<Item> OnItemClicked;
}