using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_manager : MonoBehaviour
{
    [SerializeField] button_manager button_c;
    [SerializeField] image_manager image_c;
    [SerializeField] inputfield_manager field_c;
    GameObject option_image_object;
    [SerializeField] private bool is_option_active;


    public bool is_option_prop
    {
        get { return is_option_active; }
    }

    // Start is called before the first frame update
    void Start()
    {
        option_image_object = GameObject.Find("Image_Option");
        option_image_object.SetActive(false);
        is_option_active = false;
    }
    
    public void Option_switch()
    {
        Debug.Log("Escapeƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚Ü‚µ‚½");
        if(option_image_object.activeInHierarchy)
        {
            option_image_object.SetActive(false);
            is_option_active = false;
            
        }
        else
        {
            option_image_object.SetActive(true);
            is_option_active = true;
        }
        image_c.Slide_Change_Color();
        button_c.Button_Controller();
        field_c.Field_ActiveStatus();
    }
}
