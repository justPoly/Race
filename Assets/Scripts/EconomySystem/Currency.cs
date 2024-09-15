using UnityEngine;

public class Currency
{
    private int balance;
    private const string CurrencyKey = "PlayerCurrency"; // Key to store in PlayerPrefs

    public Currency(int initialBalance)
    {
        balance = PlayerPrefs.GetInt(CurrencyKey, initialBalance); // Load saved currency
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
