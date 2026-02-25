using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy = 50f;
    [SerializeField] private float minEnergyToBoost = 0.2f;
    [SerializeField] private float energyConsumptionPerSecond = 0.5f;
    [SerializeField] private float energyRegen = 0.5f;

    void Start()
    {
        player = GetComponent<PlayerController>();

        energy = maxEnergy;
        HUDManager.Instant.UpdateEnergySlider(energy, maxEnergy);
    }

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

        HUDManager.Instant.UpdateEnergySlider(energy, maxEnergy);
    }
    public bool HasEnergy()
    {
        if (energy >= minEnergyToBoost)
            return true;
        return false;
    }
}
