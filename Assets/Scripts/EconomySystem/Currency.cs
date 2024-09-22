using UnityEngine;

public class Currency : MonoBehaviour
{
    // Singleton instance
    public static Currency Instance { get; private set; }

    private int balance;
    private const string CurrencyKey = "PlayerCurrency"; // Key to store in PlayerPrefs

    // Initialize the Singleton and ensure it persists across scenes
    private void Awake()
    {
        // If an instance already exists, destroy this object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Set this instance and ensure it doesn't get destroyed on scene load
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Initialize currency (you could call this from a GameManager or UI script)
    public void InitializeCurrency(int initialBalance)
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
            GetBalance();
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
