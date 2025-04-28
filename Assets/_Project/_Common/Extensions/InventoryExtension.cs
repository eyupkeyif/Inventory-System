using System.Collections.Generic;

public static class InventoryExtension
{
    public static void ShowItemsByType(this List<Slot> slots, ItemType itemType, bool isAllItems = false)
    {
        if(isAllItems)
        {
            foreach (var slot in slots)
            {
                slot.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var slot in slots)
            {

                if(slot.currentItem == null || itemType != slot.currentItem.GetItemData().Type)
                {
                    slot.gameObject.SetActive(false);
                }
                else
                {
                    slot.gameObject.SetActive(true);
                }
            }
        }
        
    }
}