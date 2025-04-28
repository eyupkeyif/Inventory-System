using TMPro;
using UnityEngine;

public class ItemStatsView : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI attackTMP;
    [SerializeField] TextMeshProUGUI healthTMP;
    [SerializeField] TextMeshProUGUI defenseTMP;
    [SerializeField] TextMeshProUGUI speedTMP;

    public void UpdateStats(ItemStats stats)
    {
        attackTMP.text = $"ATK : +{stats.attack}";
        healthTMP.text = $"HP : +{stats.heatlh}";
        defenseTMP.text = $"DEF : +{stats.armor}";
        speedTMP.text = $"SPD : +{stats.speed}";
    }
    
}