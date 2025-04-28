using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField] PlayerData playerData;
    private PlayerStats currentStats;

    private void Start() 
    {
        ItemService.OnItemEquipped += EquipItem;
        ItemService.OnItemUnequipped += UnequipItem;
        currentStats = playerData.playerStats;
        PlayerService.OnStatsChanged?.Invoke(currentStats);
    }
    public void SetData(PlayerData playerData)
    {
        this.playerData = playerData;
        this.currentStats = playerData.playerStats;
    }

    public void EquipItem(ItemData equippedItemData)
    {
        currentStats.health += equippedItemData.GetItemStats().heatlh;
        currentStats.attack += equippedItemData.GetItemStats().attack;
        currentStats.defense += equippedItemData.GetItemStats().armor;
        currentStats.speed += equippedItemData.GetItemStats().speed;

        PlayerService.OnStatsChanged?.Invoke(currentStats);
    }

    public void UnequipItem(ItemData unequippedItemData)
    {
        currentStats.health -= unequippedItemData.GetItemStats().heatlh;
        currentStats.attack -= unequippedItemData.GetItemStats().attack;
        currentStats.defense -= unequippedItemData.GetItemStats().armor;
        currentStats.speed -= unequippedItemData.GetItemStats().speed;

        PlayerService.OnStatsChanged?.Invoke(currentStats);
    }

}
