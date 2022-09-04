using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_B_manager : MonoBehaviour
{
    [SerializeField] Button_A_manager button_a_c;
    RectTransform canvas_size;
    RectTransform button_size;
    Image button_image;

    // Start is called before the first frame update
    void Start()
    {
        canvas_size = GameObject.Find("Canvas").GetComponent<RectTransform>();
        //Debug.Log("Screen Width : " + Screen.width);
        //Debug.Log("Screen  height: " + Screen.height);
        button_size = GetComponent<RectTransform>();
        Vector2 button_size_set = new Vector2(canvas_size.sizeDelta.x * 0.48f, canvas_size.sizeDelta.y * 0.48f);
        //Vector2 button_anchored_set = new Vector2(0f, 0f);
        button_size.sizeDelta = button_size_set;
        //button_size.anchoredPosition = button_anchored_set;
        //button_size.pivot = new Vector2(1, 1);
        button_image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        button_image.color = button_a_c.color;
    }
}
