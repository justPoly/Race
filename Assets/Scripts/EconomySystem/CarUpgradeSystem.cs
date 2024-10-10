using UnityEngine;

public class CarUpgradeSystem
{
    private CurrencyManager currencyManager;

    // Constructor that takes in the CurrencyManager scriptable object
    public CarUpgradeSystem(CurrencyManager currencySystem)
    {
        currencyManager = currencySystem;
    }

    // Method to upgrade the car if the player has enough currency
    public void UpgradeCar(CarUpgrade upgrade)
    {
        if (currencyManager != null)
        {
            if (currencyManager.SpendMoney(upgrade.cost))
            {
                Debug.Log($"{upgrade.upgradeName} upgraded for {upgrade.cost} credits!");
            }
            else
            {
                Debug.Log("Not enough credits to upgrade!");
            }
        }
        else
        {
            Debug.LogError("CurrencyManager is not initialized.");
        }
    }
}

