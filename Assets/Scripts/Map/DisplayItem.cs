using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject locked;

    public void DisplayMap(Map _map)
    {
        itemName.text = _map.mapName;
        // itemName.color = _map.nameColor;
        itemDescription.text = _map.mapDescription;
        itemImage.sprite = _map.mapImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.mapIndex;
        locked.SetActive(!mapUnlocked);
        if(mapUnlocked)
          itemImage.color = Color.white;
          else
          itemImage.color = Color.gray;
    }
}
