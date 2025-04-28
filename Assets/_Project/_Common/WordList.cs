using UnityEngine;

public static class WordList
{
    private static readonly string[] prefixes = 
    {
        "Grandma's", "Ancient", "Forgotten", "Rusty", "Shiny", "Mysterious", "Not", "King's", "Tiny", "Glorious"
    };

    private static readonly string[] suffixes = 
    {
        "of Destiny", "of Doom", "of Laughter", "of Regret", "of the Duck", "of Nightmares", "of Confusion", "of Hope"
    };

    public static string AssignName(ItemType type)
    {
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string baseName = GetBaseName(type);
        string suffix = suffixes[Random.Range(0, suffixes.Length)];

        return $"{prefix} {baseName} {suffix}";
    }

    private static string GetBaseName(ItemType type)
    {
        var baseName = "";
        switch (type)
        {
            case ItemType.Weapon: baseName = "Sword";
            break;
            case ItemType.Helmet: baseName = "Helmet";
            break;
            case ItemType.Shirt: baseName = "Vest";
            break;
            case ItemType.Boots: baseName = "Boots";
            break;
            case ItemType.Pants: baseName = "Pants";
            break;
            case ItemType.Accessory: baseName = "Necklace";
            break;

            default: break;
        }

        return baseName;
    }
}