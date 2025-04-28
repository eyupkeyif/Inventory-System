using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryView : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] Slot slot;
    [SerializeField] Item item;
    [SerializeField] Transform slotParent;
    [SerializeField] ItemInfoPanel itemInfoPanel;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    private ItemData[] Items;

    private List<Slot> slots= new List<Slot>();

    private const string Items_Path = "ItemSOs";

    public EquipmentSlot[] GetEquipmentSlots() => equipmentSlots;
    
    public EquipmentSlot GetEquipmentSlot(ItemType type)
    {
        foreach (var slot in equipmentSlots)
        {
            if(slot.ItemType == type)
            return slot;
        }

        return null;
    }

    private void Awake() 
    {
        UIService.OnFilterButtonPressed += (ItemType type, bool isAllItems) => 
        {
            slots.ShowItemsByType(type,isAllItems);
        };

        ItemService.OnItemClicked += OpenItemInfoPanel;
        UIService.OnEquipItemButtonPressed += EquipItemHandler;
        UIService.OnUnEquipItemButtonPressed += UnEquipItemHandler;
    }

    private void EquipItemHandler(Item item)
    {
        var slot = GetEquipmentSlot(item.GetItemData().Type);

        if(slot != null)
        {
            item.HandleSlotInteraction(slot);
        }

        CloseItemInfoPanel();
    }

    private void UnEquipItemHandler(Item item)
    {
        foreach (var slot in slots)
        {
           if(slot.currentItem == null)
           {
             item.HandleSlotInteraction(slot);
             CloseItemInfoPanel();
             
             return;
           } 
        }

        
    }

    private void OpenItemInfoPanel(Item item)
    {
        itemInfoPanel.SetupPanel(item);
        itemInfoPanel.gameObject.SetActive(true);
    }
    private void CloseItemInfoPanel() => itemInfoPanel.gameObject.SetActive(false);


    public void Start()
    {
        GetAllItems();

        var inventoryConfig = ConfigManager.Instance.InventoryConfig;

        for (int i = 0; i < inventoryConfig.row ; i++)
        {
            for (int j = 0; j < inventoryConfig.col ; j++)
            {
                var slotView = Instantiate(slot);
                slotView.slotIndex = i;
                slotView.transform.SetParent(slotParent,true);
                slots.Add(slotView);
            }
        }

        //Randomize item data
        for (int i = 0; i < slots.Count; i++)
        {
            var randomNumber = Random.Range(0, Items.Length);

            var randomItemData = Items[randomNumber];

            var randomItem = Instantiate(item);

            randomItem.transform.SetParent(slots[i].transform);
            randomItem.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            randomItem.Setup(randomItemData);
            randomItem.SetSlot(slots[i]);
            slots[i].currentItem = randomItem;
        }
    }


    public void GetAllItems()
    {
        var allObjects = Resources.LoadAll(Items_Path, typeof(ItemData));

        Items = new ItemData[allObjects.Length];

        for (int i = 0; i < allObjects.Length; i++)
        {
            Items[i] = (ItemData)allObjects[i];
        }

    }
}