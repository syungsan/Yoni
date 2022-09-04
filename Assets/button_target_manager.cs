using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_target_manager : MonoBehaviour
{
    [SerializeField] game_director director_c;
    RectTransform target_button;
    Vector2 target_orange;
    Vector2 target_yoni;
    // Start is called before the first frame update
    void Start()
    {
        target_button = GameObject.Find("Button_Target").GetComponent<RectTransform>();
        target_orange = new Vector2(20.0f,-10.0f);
        target_yoni = new Vector2(120.0f, 120.0f);
        target_button.anchoredPosition = target_orange;
    }

    // Update is called once per frame
    void Update()
    {
        if(director_c.task_image_indx < 4)
        {
            target_button.position = target_orange;
            Debug.Log("indx‚ª5‚æ‚è¬‚³‚¢‚Å‚·" + target_button.position);
        }
        else
        {
            target_button.position = target_yoni;
        }
    }
}
