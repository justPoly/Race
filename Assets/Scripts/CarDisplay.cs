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

    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject play;

    public void DisplayCar(Car _car)
    {
         carName.text = _car.carName;
         carDescription.text = _car.carDescription;
         carPrice.text = "$" + _car.carPrice;
         
         carSpeed.fillAmount = (float)_car.speed / 100;
         carHandling.fillAmount = (float)_car.handling / 100;
         carAcceleration.fillAmount = (float)_car.acceleration / 100;

         if(carHolder.transform.childCount > 0)
            Destroy(carHolder.transform.GetChild(0).gameObject);

            Instantiate(_car.carModel, carHolder.transform.position, carHolder.transform.rotation, carHolder.transform);
         
        bool carUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _car.carIndex;
        locked.SetActive(!carUnlocked);
        play.SetActive(carUnlocked);
        // if(carUnlocked)
        //   itemImage.color = Color.white;
        //   else
        //   itemImage.color = Color.gray;
    }
}
