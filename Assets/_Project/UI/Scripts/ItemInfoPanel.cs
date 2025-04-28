using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] ItemStatsView statsView;
    [SerializeField] Image itemIcon;
    [SerializeField] Image itemFrame;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] Button equipButton;
    [SerializeField] Button unEquipButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button deleteButton;

    private Item clickedItem;

    private void OnQuitButtonPressed() => gameObject.SetActive(false);

    private void OnUnequipButtonPressed() => UIService.OnUnEquipItemButtonPressed?.Invoke(clickedItem);

    private void OnEquipButtonPressed() => UIService.OnEquipItemButtonPressed?.Invoke(clickedItem);

    private void OnDeleteButtonPressed()
    {
        UIService.OnDeleteItemButtonPressed?.Invoke(clickedItem);
        gameObject.SetActive(false);    
    } 
    void Awake()
    {
        equipButton.onClick.AddListener(OnEquipButtonPressed);
        unEquipButton.onClick.AddListener(OnUnequipButtonPressed);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
        deleteButton.onClick.AddListener(OnDeleteButtonPressed);
    }

    

    public void SetupPanel(Item item)
    {
        var itemData = item.GetItemData();
        clickedItem = item;
        itemIcon.sprite = itemData.GetItemIcon();
        itemFrame.sprite = ConfigManager.Instance.ItemFrameConfig.GetFrame(itemData.BaseItemTier);
        itemName.text = itemData.Id;
        statsView.UpdateStats(itemData.GetItemStats());

        ButtonArrangement(item);
    }

    private void ButtonArrangement(Item item)
    {
        if(item.GetSlot() is EquipmentSlot)
        {
            unEquipButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            unEquipButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
        }
    }
}