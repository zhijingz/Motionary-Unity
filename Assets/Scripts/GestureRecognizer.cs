using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;


public class GestureRecognizer : MonoBehaviour
{
    private DollarRecognizer dollarRecognizer;
    private const float SAMPLING_FREQUENCY = 0.1f;
    private List<Vector2> gesturePoints = new List<Vector2>();
    private float _t;
    private float elapsedTime;
    private bool isRecording = false;

    public float screenW = Screen.width;
    public float screenH = Screen.height;


    private readonly object _resultLock = new object();

    void Awake()
    {
        dollarRecognizer = new DollarRecognizer();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  // Press 'R' to start recording gesture
        {
            isRecording = true;
            gesturePoints.Clear();
            elapsedTime = 0f;
            Debug.Log("Started recording gesture points...");

        }

        if (gesturePoints.Count >= 40)
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
