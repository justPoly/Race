using UnityEngine;

[CreateAssetMenu(fileName = "Currency", menuName = "Managers/Currency Manager", order = 1)]

public class CurrencyManager : ScriptableObject
{
    [SerializeField] private int balance;
    private const string CurrencyKey = "PlayerCurrency"; // Key to store in PlayerPrefs

    // Initialize the currency balance (load from PlayerPrefs or set default)
    public void InitializeCurrency(int initialBalance)
    {
        balance = PlayerPrefs.GetInt(CurrencyKey, initialBalance);
    }

    public int GetBalance()
    {
        return balance;
    }

    public bool AddMoney(int amount)
    {
        if (amount > 0)
        {
            balance += amount;
            SaveCurrency();
            return true;
        }
        return false;
    }

    public bool SpendMoney(int amount)
    {
        if (amount > 0 && balance >= amount)
        {
            balance -= amount;
            SaveCurrency();
            return true;
        }
        return false;
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt(CurrencyKey, balance);  // Save the balance
        PlayerPrefs.Save();  // Ensure the data is written to disk
    }
}
