using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Frame", menuName = "Item Data/Item Frame")]
public class ItemFrameConfig : ScriptableObject
{
    public ItemFrame[] Frames;

    public Sprite GetFrame(ItemTier tier)
    {
        foreach (var itemFrame in Frames)
        {
            if(itemFrame.Tier == tier)
            {
                return itemFrame.Frame;
            }
        }

        return null;
    }
}

[Serializable]
public class ItemFrame
{
    public ItemTier Tier;
    public Sprite Frame;
}