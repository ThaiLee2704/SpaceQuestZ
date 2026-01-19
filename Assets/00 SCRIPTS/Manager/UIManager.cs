using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.maxValue = max;
        energySlider.value = Mathf.RoundToInt(current);
        energyText.text = energySlider.value + "/" + energySlider.maxValue;
    }
    
    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = Mathf.RoundToInt(current);
        healthText.text = healthSlider.value + "/" + healthSlider.maxValue;
    }
}
