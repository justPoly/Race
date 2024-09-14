using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomInput : MonoBehaviour
{
 public RectTransform uiElementToMove;
 public float moveDistance = 208.0f; // Adjust the distance to move

    private Vector2 originalAnchoredPosition;

    private void Start()
    {
        // Store the original anchored position of the UI element
        originalAnchoredPosition = uiElementToMove.anchoredPosition;
    }

    void Update()
    {
        if(TouchScreenKeyboard.visible) {
          Vector2 newAnchoredPosition = originalAnchoredPosition + Vector2.up * moveDistance;
          uiElementToMove.anchoredPosition = newAnchoredPosition;
        } else {
            uiElementToMove.anchoredPosition = originalAnchoredPosition;
        }
    }
   
}
