using UnityEngine;

public abstract class Slot : MonoBehaviour 
{
    public Item currentItem;
    public int slotIndex;
    public void SetId(int id)
    {
        slotIndex = id;
    }
}