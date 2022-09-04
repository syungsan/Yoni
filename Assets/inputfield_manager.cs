using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inputfield_manager : MonoBehaviour
{
    [SerializeField] game_director director_c;
    [SerializeField] image_manager image_c;
    [SerializeField] Option_manager option_c;
    GameObject Enter_ID_Field_Object;
    TMP_InputField Enter_ID_Field;
    public string ID;

    // Start is called before the first frame update
    void Start()
    {
        Enter_ID_Field_Object = GameObject.Find("InputField (TMP)");
        Enter_ID_Field = Enter_ID_Field_Object.GetComponent<TMP_InputField>();
        Enter_ID_Field.Select();

    }

    //game_directorで呼ぶフィールド系の関数
    public void Field_Controller()
    {
        Field_InputStatus();
        Field_ActiveStatus();
        Select_Field();
    }

    //学生番号の取得
    public void InputID()
    {
        ID = Enter_ID_Field.text;
        Debug.Log(ID);
    }

    //文字の入力制限
    public void Field_InputStatus()
    {
        if(director_c.scene_counter == 1 && director_c.task_image_indx == 4)
        {
            Enter_ID_Field.interactable = false;
        }
        else
        {
            Enter_ID_Field.interactable = true;
        }
    }

    //フィールドの表示切替
    public void Field_ActiveStatus()
    {
        if(director_c.scene_counter == 1 && option_c.is_option_prop == false && director_c.task_image_indx > 2)
        {
            Enter_ID_Field_Object.SetActive(true); 
        }
        else
        {
            Enter_ID_Field_Object.SetActive(false);
        }
    }

    //フィールドがある時にアクティブにする
    public void Select_Field()
    {
        Debug.Log(Enter_ID_Field_Object.activeSelf);
        if (Enter_ID_Field_Object.activeSelf == true)
        {
            Enter_ID_Field.Select();
        }
    }


    //Enterが押されたときにつぎのスライドに進む
    public void Input_Enterkey()
    {
        InputID();
        if (director_c.scene_counter == 1 && director_c.task_image_indx == 3 && ID != "")
        {
            if((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
            {
                director_c.task_image_indx++;
                director_c.Main_Controller();
                //director_c.Flugs_slide_changer = true;
            }
        }
    }

    //フィールドの中身を消す
    public void Delete_Field()
    {
        Enter_ID_Field.text = "";
    }

}
