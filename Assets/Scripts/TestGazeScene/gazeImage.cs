using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class gazeImage : MonoBehaviour
{

    [SerializeField] GameObject gaze_object;
    RectTransform gaze_position;
    GazePoint gazePoint;
    private Vector2 roundedSampleInput;
    private Vector2 gazePosition;



    // Start is called before the first frame update
    void Start()
    {

        gaze_position = gaze_object.GetComponent<RectTransform>();
        gazePoint = TobiiAPI.GetGazePoint();
    }

    // Update is called once per frame
    void Update()
    {
        gazePosition = gazePoint.Screen;
        //roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));

        gaze_position.position = gazePosition;
        Debug.Log(gazePosition);
    }
}
