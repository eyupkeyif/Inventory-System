using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] Transform content;
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

    void OnEnable()
    {
        DOTween.Kill(this);
        content.localScale = Vector3.one * 0.2f;
        content.DOScale(Vector3.one,0.3f)
        .SetEase(Ease.OutBack)
        .SetId(this)
        .SetLink(gameObject,LinkBehaviour.KillOnDisable);
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