using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.value = Mathf.RoundToInt(current);
        energySlider.maxValue = max;
        energyText.text = energySlider.value + "/" + energySlider.maxValue;

    }
}
