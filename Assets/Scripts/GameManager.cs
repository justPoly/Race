using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CarUpgradeSystem carUpgradeSystem;

    [SerializeField] private CarUpgrade engineUpgrade;
    [SerializeField] private CarUpgrade tiresUpgrade;

    private const string EngineUpgradeKey = "EngineUpgradePurchased";
    private const string TiresUpgradeKey = "TiresUpgradePurchased";

    void Start()
    {
        // Initialize the Currency singleton with an initial balance of 1000 (or use saved balance)
        Currency.Instance.InitializeCurrency(1000);

        carUpgradeSystem = new CarUpgradeSystem(Currency.Instance);

        // Load saved upgrade state when the game starts
        LoadUpgradeState();

        // Example usage: Upgrade car based on save data
        if (IsUpgradePurchased(EngineUpgradeKey))
        {
            Debug.Log("Engine already upgraded");
        }
        else
        {
            UpgradeCar(engineUpgrade);
        }
    }

    public int GetCurrencyBalance()
    {
        return Currency.Instance.GetBalance();
    }

    public void UpgradeCar(CarUpgrade upgrade)
    {
        carUpgradeSystem.UpgradeCar(upgrade);
        Debug.Log($"Current Balance: {Currency.Instance.GetBalance()} credits.");

        // Save upgrade state after purchasing
        SaveUpgradeState(upgrade.upgradeName);
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

        Currency.Instance.AddMoney(reward);
        Debug.Log($"Player finished in position {position}, rewarded {reward} credits!");
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
