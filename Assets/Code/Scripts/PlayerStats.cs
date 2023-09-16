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

    private void OnEnable()
    {
        PlayerMaxHealthIncreaser.OnPlayerHealthIncrease += PlayerMaxHealthPowerUp;
        PlayerMaxHealthIncreaser.OnPlayerHealthIncrease_2 += PlayerMaxHealthPowerUp_2;
    }

    private void OnDisable()
    {
        PlayerMaxHealthIncreaser.OnPlayerHealthIncrease += PlayerMaxHealthPowerUp;
        PlayerMaxHealthIncreaser.OnPlayerHealthIncrease_2 += PlayerMaxHealthPowerUp_2;
    }

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

    public void PlayerMaxHealthPowerUp()
    {
        Debug.Log("PlayerMaxHealth Increase");
        maximumHealth = maximumHealth + 20;
        currentHealth = maximumHealth;
    }

    public void PlayerMaxHealthPowerUp_2()
    {
        Debug.Log("PlayerMaxHealth Increase 2");
        maximumHealth = maximumHealth + 40;
        currentHealth = maximumHealth;
    }
}
