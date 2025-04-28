using UnityEngine;
using UnityEngine.UI;

public class FilterButtons : MonoBehaviour 
{
    [Header("Filter Buttons")]
    [SerializeField] Button allButton;
    [SerializeField] Button weaponButton;
    [SerializeField] Button helmetButton;
    [SerializeField] Button accessoryButton;
    [SerializeField] Button bootsButton;
    [SerializeField] Button shirtButton;
    [SerializeField] Button pantsButton;
    private Button activeButton;

    [Header("Button Sprites")]
    [SerializeField] Sprite selectedButtonSprite;
    [SerializeField] Sprite unselectedButtonSprite;

    private void Awake() 
    {
        allButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Weapon,allButton,true);});
        weaponButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Weapon,weaponButton);});
        helmetButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Helmet,helmetButton);});
        accessoryButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Accessory,accessoryButton);});
        bootsButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Boots,bootsButton);});
        shirtButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Shirt,shirtButton);});
        pantsButton.onClick.AddListener(() => {OnFilterButtonClicked(ItemType.Pants,pantsButton);});

        activeButton = allButton;
        activeButton.image.sprite = selectedButtonSprite;

    }

    private void OnFilterButtonClicked(ItemType type,Button currentPressedButton, bool isAll = false)
    {
        activeButton.image.sprite = unselectedButtonSprite;
        UIService.OnFilterButtonPressed?.Invoke(type,isAll);
        activeButton = currentPressedButton;
        activeButton.image.sprite=selectedButtonSprite;
    }


    
}