using UnityEngine;

public class ChangeItemSO : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private DisplayItem display;
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
    }
}
