using System;

public static class UIService
{
    public static Action<ItemType,bool> OnFilterButtonPressed;
    public static Action<Item> OnDeleteItemButtonPressed;
    public static Action<Item> OnEquipItemButtonPressed;
    public static Action<Item> OnUnEquipItemButtonPressed;
}