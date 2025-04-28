using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler

{
    [Header("References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemFrame;

    private ItemData itemData;
    private Transform originalParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Slot currentSlot;
    private bool isDragging;
    Vector2 clickedPosition;
    float clickedPointTreshold = 0.1f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        UIService.OnDeleteItemButtonPressed += DeleteItemHandler;
    }

    private void DeleteItemHandler(Item item)
    {
        if(item != this) return;

        currentSlot.currentItem = null;
        Destroy(gameObject);
    }

    //Random tier setup
    public void Setup(ItemData data)
    {
        itemData = Instantiate(data);
        int randomTierIndex = Random.Range(0, data.TierData.Length);
        itemData.BaseItemTier = itemData.TierData[randomTierIndex].Tier;
        itemData.Id = WordList.AssignName(itemData.Type);

        UpdateVisuals();
    }

    public void SetSlot(Slot slot) => currentSlot = slot;
    public Slot GetSlot() => currentSlot;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        clickedPosition = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (eventData != null)
            transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
        Slot possibleSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();

        if (eventData?.pointerEnter != null && possibleSlot)
        {
            HandleSlotInteraction(possibleSlot);
        }
        else
        {
            ReturnToOriginalParent();
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isDragging && Vector2.Distance(clickedPosition, eventData.position) <= clickedPointTreshold)
        {
            ItemService.OnItemClicked?.Invoke(this);
        }
    }

    public void HandleSlotInteraction(Slot possibleSlot)
    {
        
        if(possibleSlot == currentSlot)
        {
            ReturnToOriginalParent();
            return;
        }

        if (possibleSlot is InventorySlot inventorySlot)
        {
            HandleInventorySlotInteraction(inventorySlot);
        }
        else if (possibleSlot is EquipmentSlot equipmentSlot)
        {
            HandleEquipmentSlotInteraction(equipmentSlot);
        }
        else
        {
            ReturnToOriginalParent();
        }
    }

    private void HandleInventorySlotInteraction(InventorySlot targetSlot)
    {
        if (targetSlot.currentItem != null)
        {
            if (CanMerge(this, targetSlot.currentItem) && currentSlot is InventorySlot)
            {
                MergeWith(targetSlot.currentItem, currentSlot);
            }
            else
            {
                if(currentSlot is EquipmentSlot equipmentSlot)
                {
                    if(equipmentSlot.currentItem.itemData.Type == targetSlot.currentItem.itemData.Type)
                    {
                        UnequipItem();
                        targetSlot.currentItem.EquipItem();

                        SwapItems(currentSlot, targetSlot);
                    }
                    else
                    {
                        ReturnToOriginalParent();
                    }
                }
                else
                {
                    SwapItems(currentSlot, targetSlot);
                }
                
            }
        }
        else
        {
            if (currentSlot is EquipmentSlot)
                UnequipItem();

            currentSlot.currentItem = null;
        }

        targetSlot.currentItem = this;
        currentSlot = targetSlot;
        transform.SetParent(currentSlot.transform, true);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void HandleEquipmentSlotInteraction(EquipmentSlot targetSlot)
    {
        if (targetSlot.ItemType == itemData.Type)
        {
            if (targetSlot.currentItem != null)
            {
                targetSlot.currentItem.UnequipItem();
                SwapItems(currentSlot, targetSlot);
            }
            else
            {
                if (currentSlot is EquipmentSlot)
                    UnequipItem();
                else
                    currentSlot.currentItem = null;
            }

            EquipItem();
            targetSlot.currentItem = this;
            currentSlot = targetSlot;
            transform.SetParent(currentSlot.transform, true);
        }
        else
        {
            ReturnToOriginalParent();
        }
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ReturnToOriginalParent()
    {
        if (originalParent != null)
        {
            transform.SetParent(originalParent, true);
        }
    }

    private void SwapItems(Slot currentSlot, Slot targetSlot)
    {
        targetSlot.currentItem.transform.SetParent(currentSlot.transform, true);
        targetSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        targetSlot.currentItem.SetSlot(currentSlot);
        currentSlot.currentItem = targetSlot.currentItem;
    }

    private bool CanMerge(Item itemA, Item itemB)
    {
        return itemA.itemData.Type == itemB.itemData.Type &&
               itemA.itemData.BaseItemTier == itemB.itemData.BaseItemTier &&
               itemA.itemData.BaseItemTier != ItemTier.Tier4;
    }

    private void MergeWith(Item otherItem, Slot currentSlot)
    {
        Destroy(otherItem.gameObject);
        currentSlot.currentItem = null;
        itemData.BaseItemTier++;
        // itemData.UpdateStatsForTier(itemData.BaseItemTier); // Make sure this function exists in ItemData
        UpdateVisuals();

        // You can add a merge animation call here later, like:
        // StartCoroutine(MergeAnimation());
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    private void UpdateVisuals()
    {
        itemIcon.sprite = itemData.GetItemIcon();
        itemFrame.sprite = ConfigManager.Instance.ItemFrameConfig.GetFrame(itemData.BaseItemTier);
    }

    private void EquipItem()
    {
        ItemService.OnItemEquipped?.Invoke(itemData);
    }

    private void UnequipItem()
    {
        ItemService.OnItemUnequipped?.Invoke(itemData);
    }

}
