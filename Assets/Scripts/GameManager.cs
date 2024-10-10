using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CarUpgradeSystem carUpgradeSystem;
    
    [SerializeField] private CarUpgrade engineUpgrade;
    [SerializeField] private CarUpgrade tiresUpgrade;

    [SerializeField] private CurrencyManager currencyManager; // Reference to the CurrencyManager ScriptableObject

    private const string EngineUpgradeKey = "EngineUpgradePurchased";
    private const string TiresUpgradeKey = "TiresUpgradePurchased";

    void Start()
    {
        if (currencyManager != null)
        {
            currencyManager.InitializeCurrency(1000); // Initialize currency with 1000 credits
            carUpgradeSystem = new CarUpgradeSystem(currencyManager); // Use the CurrencyManager instance

            // Load saved upgrade state when the game starts
            LoadUpgradeState();
        }
        else
        {
            Debug.LogError("CurrencyManager is not assigned!");
        }
    }

    public int GetCurrencyBalance()
    {
        if (currencyManager != null)
        {
            return currencyManager.GetBalance(); // Use the CurrencyManager to get balance
        }

        Debug.LogError("CurrencyManager is not assigned!");
        return 0;
    }

    public void UpgradeCar(CarUpgrade upgrade)
    {
        if (carUpgradeSystem != null)
        {
            carUpgradeSystem.UpgradeCar(upgrade);
            Debug.Log($"Current Balance: {currencyManager.GetBalance()} credits.");

            // Save upgrade state after purchasing
            SaveUpgradeState(upgrade.upgradeName);
        }
        else
        {
            Debug.LogError("CarUpgradeSystem is not initialized!");
        }
    }

    public void SimulateRace(int position)
    {
        int reward = 0;
        switch (position)
        {
            case 1: reward = 500; break;
            case 2: reward = 300; break;
            case 3: reward = 100; break;
            default: reward = 50; break;
        }

        if (currencyManager != null)
        {
            currencyManager.AddMoney(reward);
            Debug.Log($"Player finished in position {position}, rewarded {reward} credits!");
        }
        else
        {
            Debug.LogError("CurrencyManager is not assigned!");
        }
    }

    // Save the upgrade state after a successful upgrade
    void SaveUpgradeState(string upgradeName)
    {
        if (upgradeName == engineUpgrade.upgradeName)
        {
            PlayerPrefs.SetInt(EngineUpgradeKey, 1); // 1 = purchased
        }
        else if (upgradeName == tiresUpgrade.upgradeName)
        {
            PlayerPrefs.SetInt(TiresUpgradeKey, 1); // 1 = purchased
        }

        PlayerPrefs.Save(); // Save the data
    }

    // Check if a specific upgrade has been purchased
    bool IsUpgradePurchased(string upgradeKey)
    {
        return PlayerPrefs.GetInt(upgradeKey, 0) == 1; // 0 = not purchased
    }

    // Load the saved upgrade state
    void LoadUpgradeState()
    {
        if (IsUpgradePurchased(EngineUpgradeKey))
        {
            Debug.Log("Engine upgrade previously purchased");
        }

        if (IsUpgradePurchased(TiresUpgradeKey))
        {
            Debug.Log("Tires upgrade previously purchased");
        }
    }
}
