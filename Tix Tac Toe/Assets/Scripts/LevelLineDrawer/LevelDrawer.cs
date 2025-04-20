using UnityEngine;

public class LevelDrawer : MonoBehaviour
{
    [Header("Line Width")]

    [Range(0f, 1f)] [SerializeField] private float topLineWidth;
    [Range(0f, 1f)] [SerializeField] private float downLineWidth;
    [Range(0f, 1f)] [SerializeField] private float leftLineWidth;
    [Range(0f, 1f)] [SerializeField] private float rightLineWidth;

    [Header("Line Coordinate")]

    [SerializeField] private float topLineStartX;
    [SerializeField] private float topLineStartY, topLineEndX,  topLineEndY;
    [SerializeField] private float downLineStartX, downLineStartY, downLineEndX, downLineEndY;
    [SerializeField] private float leftLineStartX, leftLineStartY, leftLineEndX, leftLineEndY;
    [SerializeField] private float rightLineStartX,rightLineStartY,rightLineEndX,rightLineEndY;

    [Header("Game Objects")]

    [SerializeField] private GameObject gameObject1;
    [SerializeField] private GameObject gameObject2;
    [SerializeField] private GameObject gameObject3;
    [SerializeField] private GameObject gameObject4;

    private LineRenderer lineHorizontalTop;
    private LineRenderer lineHorizontalDown;
    private LineRenderer lineVertikalLeft;
    private LineRenderer lineVertikalRight;

    private void Awake()
    {
        // LineRenderer-Komponente hinzufügen
        lineHorizontalTop = gameObject1.AddComponent<LineRenderer>();
        lineHorizontalDown = gameObject2.AddComponent<LineRenderer>();
        lineVertikalLeft = gameObject3.AddComponent<LineRenderer>();
        lineVertikalRight = gameObject4.AddComponent<LineRenderer>();

        // Material für die Linie (kann ein Standardmaterial sein)
        lineHorizontalTop.material = new Material(Shader.Find("Sprites/Default"));
        lineHorizontalDown.material = new Material(Shader.Find("Sprites/Default"));
        lineVertikalLeft.material = new Material(Shader.Find("Sprites/Default"));
        lineVertikalRight.material = new Material(Shader.Find("Sprites/Default"));
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        // Farbe der Linie
        lineHorizontalTop.startColor = Color.red;
        lineHorizontalTop.endColor = Color.blue;
        lineHorizontalDown.startColor = Color.red;
        lineHorizontalDown.endColor = Color.blue;
        lineVertikalLeft.startColor = Color.red;
        lineVertikalLeft.endColor = Color.blue;
        lineVertikalRight.startColor = Color.red;
        lineVertikalRight.endColor = Color.blue;

        // Breite der Linie
        lineHorizontalTop.startWidth = topLineWidth;
        lineHorizontalTop.endWidth = topLineWidth;
        lineHorizontalDown.startWidth = downLineWidth;
        lineHorizontalDown.endWidth = downLineWidth;
        lineVertikalLeft.startWidth = leftLineWidth;
        lineVertikalLeft.endWidth = leftLineWidth;
        lineVertikalRight.startWidth = rightLineWidth;
        lineVertikalRight.endWidth = rightLineWidth;

        // Anzahl der Punkte
        lineHorizontalTop.positionCount = 2;
        lineHorizontalDown.positionCount = 2;
        lineVertikalLeft.positionCount = 2;
        lineVertikalRight.positionCount = 2;

        // Punkte der Linie setzen
        lineHorizontalTop.SetPosition(0, new Vector3(topLineStartX, topLineStartY, 0)); // Startpunkt
        lineHorizontalTop.SetPosition(1, new Vector3(topLineEndX, topLineEndY, 0)); // Endpunkt
        lineHorizontalDown.SetPosition(0, new Vector3(downLineStartX, downLineStartY, 0)); // Startpunkt
        lineHorizontalDown.SetPosition(1, new Vector3(downLineEndX, downLineEndY, 0)); // Endpunkt
        lineVertikalLeft.SetPosition(0, new Vector3(leftLineStartX, leftLineStartY, 0)); // Startpunkt
        lineVertikalLeft.SetPosition(1, new Vector3(leftLineEndX, leftLineEndY, 0)); // Endpunkt
        lineVertikalRight.SetPosition(0, new Vector3(rightLineStartX, rightLineStartY, 0)); // Startpunkt
        lineVertikalRight.SetPosition(1, new Vector3(rightLineEndX, rightLineEndY, 0)); // Endpunkt
    }
}
