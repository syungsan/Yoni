using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_D_manager : MonoBehaviour
{

    [SerializeField] Button_A_manager button_a_c;
    RectTransform canvas_size;
    RectTransform button_size;
    Image button_image;

    // Start is called before the first frame update
    void Start()
    {
        canvas_size = GameObject.Find("Canvas").GetComponent<RectTransform>();
        button_size = GetComponent<RectTransform>();
        Vector2 button_size_set = new Vector2(canvas_size.sizeDelta.x * 0.48f, canvas_size.sizeDelta.y * 0.48f);
        button_size.sizeDelta = button_size_set;
        button_image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        button_image.color = button_a_c.color;
    }
}