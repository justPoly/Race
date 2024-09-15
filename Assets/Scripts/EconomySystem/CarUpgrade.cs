using UnityEngine;

[CreateAssetMenu(fileName = "CarUpgrade", menuName = "Upgrades/CarUpgrade", order = 1)]
public class CarUpgrade : ScriptableObject
{
    public string upgradeName;
    public int cost;
}

