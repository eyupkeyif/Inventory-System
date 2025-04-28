using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour 
{
    public StatType statType;
    public Image fillImage;
    public float maxValue;

    public void UpdateStat(float amount)
    {
        DOTween.Kill(this);
        
        var clampedAmount = Mathf.Clamp01(amount/maxValue);

        fillImage.DOFillAmount(clampedAmount, 0.3f);
    }
}