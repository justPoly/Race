using UnityEngine;

public class CarUpgradeSystem
{
    private Currency currency;

    public CarUpgradeSystem(Currency currencySystem)
    {
        currency = currencySystem;
    }

    public void UpgradeCar(CarUpgrade upgrade)
    {
        if (currency.SpendMoney(upgrade.cost))
        {
            Debug.Log($"{upgrade.upgradeName} upgraded for {upgrade.cost} credits!");
        }
        else
        {
            Debug.Log("Not enough credits to upgrade!");
        }
    }
}

