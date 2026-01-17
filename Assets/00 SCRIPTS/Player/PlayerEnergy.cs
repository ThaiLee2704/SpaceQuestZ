using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    public float CurrentEnergy => energy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerController>();

        energy = maxEnergy;
        GameManager.Instant.UIManager.UpdateEnergySlider(energy, maxEnergy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.IsPlayerBoosting)
        {
            if (energy >= 0.2f) 
                energy -= 0.2f;
            else
                player.IsPlayerBoosting = false;
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }

        GameManager.Instant.UIManager.UpdateEnergySlider(energy, maxEnergy);
    }
}
