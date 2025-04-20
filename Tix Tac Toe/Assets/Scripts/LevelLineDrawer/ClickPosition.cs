using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour
{
    protected Vector3 mousePosition;

    private Object X;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linke Maustaste
        {
            Vector3 inputMousePosition = Input.mousePosition;
            this.mousePosition = Camera.main.ScreenToWorldPoint(inputMousePosition);
            this.mousePosition.z = 0; // In 2D kann der Z-Wert ignoriert werden
            Debug.Log("Mausposition (Weltkoordinaten): " + inputMousePosition);
        }
    }
}
