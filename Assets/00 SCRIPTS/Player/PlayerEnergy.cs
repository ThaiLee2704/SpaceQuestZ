using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy = 50f;
    [SerializeField] private float energyRegen = 0.2f;
    [SerializeField] private float energyConsumptionPerSecond = 0.2f;

    public float CurrentEnergy => energy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerController>();

        energy = maxEnergy;
        UIManager.Instant.UpdateEnergySlider(energy, maxEnergy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && player.IsPlayerBoosting)
        {
            energy -= energyConsumptionPerSecond;
            if (energy <= 0f)
            {
                energy = 0f;
                player.ForceStopBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
                if (energy > maxEnergy) energy = maxEnergy;
            }
        }

        UIManager.Instant.UpdateEnergySlider(energy, maxEnergy);
    }
}
