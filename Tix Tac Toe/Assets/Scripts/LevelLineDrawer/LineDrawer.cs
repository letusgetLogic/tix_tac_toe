using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer-Komponente hinzufügen
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Material für die Linie (kann ein Standardmaterial sein)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Farbe der Linie
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // Breite der Linie
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Anzahl der Punkte
        lineRenderer.positionCount = 2;

        // Punkte der Linie setzen
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0)); // Startpunkt
        lineRenderer.SetPosition(1, new Vector3(5, 5, 0)); // Endpunkt
    }
}

