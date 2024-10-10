using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private TextMeshProUGUI carName;
    [SerializeField] private TextMeshProUGUI carDescription;
    [SerializeField] private TextMeshProUGUI carPrice;

    [Header("Stats")]
    [SerializeField] private Image carSpeed;
    [SerializeField] private Image carAcceleration;
    [SerializeField] private Image carHandling;

    [Header("3D Model")]
    [SerializeField] private GameObject carHolder;

    [Header("UI Elements")]
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject paintUpgrade;
    [SerializeField] private GameObject tireUpgrade;

    private Car currentCar;

    private CurrencyManager currencyManager;

    private void Start()
    {
        // Get the reference to the CurrencyManager from GameStateManager
        currencyManager = GameStateManager.CharacterManager;

        if (currencyManager == null)
        {
            Debug.LogError("CurrencyManager not found.");
        }
    }

    public void DisplayCar(Car _car)
    {
        currentCar = _car;

        // Update the car's name, description, and price
        carName.text = _car.carName;
        carDescription.text = _car.carDescription;
        carPrice.text = _car.carPrice.ToString();

        // Update the car's stats
        carSpeed.fillAmount = (float)_car.speed / 100;
        carHandling.fillAmount = (float)_car.handling / 100;
        carAcceleration.fillAmount = (float)_car.acceleration / 100;

        // Replace the current 3D car model if one exists
        if (carHolder.transform.childCount > 0)
            Destroy(carHolder.transform.GetChild(0).gameObject);

        Instantiate(_car.carModel, carHolder.transform.position, carHolder.transform.rotation, carHolder.transform);

        // Check if the car is unlocked based on the player's progress (using the car index)
        bool carUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _car.carIndex;
        locked.SetActive(!carUnlocked);
        play.SetActive(carUnlocked);
        paintUpgrade.SetActive(carUnlocked);
        tireUpgrade.SetActive(carUnlocked);
        unlockButton.SetActive(!carUnlocked);

        // Set up unlock button to trigger purchase if car is locked
        if (!carUnlocked)
        {
            unlockButton.GetComponent<Button>().onClick.RemoveAllListeners(); // Remove any previous listeners
            unlockButton.GetComponent<Button>().onClick.AddListener(PurchaseCar);
        }
    }

    // Attempt to purchase the car
    private void PurchaseCar()
    {
        if (currencyManager != null)
        {
            int carPriceValue = currentCar.carPrice;

            if (currencyManager.SpendMoney(carPriceValue))
            {
                Debug.Log("Car purchased successfully!");
                // Unlock the car
                PlayerPrefs.SetInt("currentScene", currentCar.carIndex); // Unlock the car in PlayerPrefs
                PlayerPrefs.Save();

                // Update UI after purchase
                locked.SetActive(false);
                play.SetActive(true);
                paintUpgrade.SetActive(true);
                tireUpgrade.SetActive(true);
                unlockButton.SetActive(false);

                Debug.Log($"New Balance: {currencyManager.GetBalance()} credits.");
            }
            else
            {
                Debug.Log("Not enough currency to purchase this car.");
            }
        }
        else
        {
            Debug.LogError("CurrencyManager is not initialized.");
        }
    }
}
