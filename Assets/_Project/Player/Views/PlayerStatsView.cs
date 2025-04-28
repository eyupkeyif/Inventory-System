using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsView : MonoBehaviour 
{
    
    [Header("Reference")]
    [SerializeField] StatBar[] statBars;

    void Awake()
    {
        PlayerService.OnStatsChanged += UpdateStatBars;
    }

    private void UpdateStatBars(PlayerStats playerStats)
    {

        foreach (var bar in statBars)
        {
            switch (bar.statType)
            {
                case StatType.Attack : bar.UpdateStat(playerStats.attack);
                break;
                case StatType.Health : bar.UpdateStat(playerStats.health);
                break;
                case StatType.Armor : bar.UpdateStat(playerStats.defense);
                break;
                case StatType.Speed : bar.UpdateStat(playerStats.speed);   
                break;

                default: break;
            }
        }
    }

    
}