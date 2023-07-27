using UnityEngine;

public class ChangeItemSO : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableObject[] scriptableObjects;

    [Header("Display Scripts")]
    [SerializeField] private DisplayItem display;
    [SerializeField] private CarDisplay carDisplay;

    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableObject(0);
    }

    public void ChangeScriptableObject(int _change)
    {
        currentIndex += _change;
        if(currentIndex < 0) 
           currentIndex = scriptableObjects.Length - 1;
        else if (currentIndex > scriptableObjects.Length - 1) 
           currentIndex = 0;

        if(display != null) display.DisplayMap((Map)scriptableObjects[currentIndex]);
        if(carDisplay != null) carDisplay.DisplayCar((Car)scriptableObjects[currentIndex]);
    }
}
