using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float currentChargedShotFuel;
    public float maximumChargedShotFuel;
    public float currentPlasmaShotFuel;
    public float maximumPlasmaShotFuel;
    public float currentHealth;
    public float maximumHealth;

    private void Start()
    {
        currentHealth = maximumHealth;
        currentChargedShotFuel = maximumChargedShotFuel;
        currentPlasmaShotFuel = maximumPlasmaShotFuel;
    }
    private void Update()
    {
        if (currentChargedShotFuel < 0)
        {
            currentChargedShotFuel = 0;
        }
    }
    public void DecreaseChargedShotFuel()
    {
        currentChargedShotFuel--;
    }

    public void RestoreHealth()
    {
        currentHealth = maximumHealth;
    }
}
