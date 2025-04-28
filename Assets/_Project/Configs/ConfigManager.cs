using System;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager> 
{
    [Header("Configs")]
    public InventoryConfig InventoryConfig;
    public ItemFrameConfig ItemFrameConfig;

}