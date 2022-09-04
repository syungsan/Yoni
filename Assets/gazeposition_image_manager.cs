using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gazeposition_image_manager : MonoBehaviour
{
    [SerializeField] GetGazePosition gaze_c;
    [SerializeField] GameObject gaze_object;
    RectTransform gaze_position;
    // Start is called before the first frame update
    void Start()
    {
        gaze_position = gaze_object.GetComponent<RectTransform>();
        GazeImage_hide();

    }

    // Update is called once per frame
    void Update()
    {
        gaze_position.position = gaze_c.View_Position();
    }

    public void GazeImage_show()
    {
        gaze_object.SetActive(true);

    }

    public void GazeImage_hide()
    {
        gaze_object.SetActive(false);
    }
}
