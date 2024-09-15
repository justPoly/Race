using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameManager gameManager;
    public TMP_Text balanceText; 
    public Button engineUpgradeButton;
    public Button tiresUpgradeButton;

    public CarUpgrade engineUpgrade; 
    public CarUpgrade tiresUpgrade;
    
    void Start()
    {
        engineUpgradeButton.onClick.AddListener(() => gameManager.UpgradeCar(engineUpgrade));
        tiresUpgradeButton.onClick.AddListener(() => gameManager.UpgradeCar(tiresUpgrade));
    }

    void Update()
    {
        balanceText.text = $"{gameManager.GetCurrencyBalance()}";
    }
}

