using UnityEngine;

[System.Serializable]
public abstract class ItemData : ScriptableObject
{
    public string Id;
    public ItemType Type;
    public ItemTier BaseItemTier;
    public ItemTierData[] TierData;

    public ItemStats GetItemStats()
    {
        foreach (var data in TierData)
        {
            if(data.Tier == BaseItemTier)
            {
                return data.Stats;
            }
        }

        return default;
    }
    public Sprite GetItemIcon()
    {
        foreach (var data in TierData)
        {
            if(data.Tier == BaseItemTier)
            {
                return data.Icon;
            }
        }

        return default;
    }
}

[System.Serializable]
public class ItemTierData
{
    public Sprite Icon;
    public ItemTier Tier;
    public ItemStats Stats;
}

