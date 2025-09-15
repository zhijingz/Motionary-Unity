using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;



public class GestureRecognizer : MonoBehaviour
{
    GestureLibrary gestureLibrary;
    
    private DollarRecognizer dollarRecognizer;
    private const float SAMPLING_FREQUENCY = 0.1f;
    private List<Vector2> gesturePoints = new List<Vector2>();
    private float _t;
    private float elapsedTime;
    private bool isRecording = false;

    public float screenW = Screen.width;
    public float screenH = Screen.height;


    void Awake()
    {
        gestureLibrary = GetComponent<GestureLibrary>();
        dollarRecognizer = new DollarRecognizer();
    }
    void Start()
    {
        dollarRecognizer.SavePattern("circle", gestureLibrary.GetCirclePoints());
        dollarRecognizer.SavePattern("star", gestureLibrary.GetStarPoints());
        dollarRecognizer.SavePattern("square", gestureLibrary.GetSquarePoints());
    }
    void Update()
    {


        if (gesturePoints.Count >= 50)
        {
            RecognizeGesture();
        }
    }

    public void AddPoint(Vector2 point) {
        gesturePoints.Add(point);
    }

    private void RecognizeGesture()
    {
        var result = dollarRecognizer.Recognize(gesturePoints);
        if (result.Match != null)
        {
            Debug.Log($"Recognized gesture: {result.Match.Name} with score {result.Score}");
        }
        else
        {
            Debug.Log("No gesture recognized");
            
        }
        // Clear for next recording
        gesturePoints.Clear();
        elapsedTime = 0f;

    }
}
