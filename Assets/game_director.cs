using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class game_director : MonoBehaviour
{
    [SerializeField] image_manager image_c;
    [SerializeField] button_manager button_c;
    [SerializeField] inputfield_manager field_c;
    [SerializeField] GetGazePosition gaze_c;
    [SerializeField] write_csv write_c;
    [SerializeField] GameObject option_image_object;
    [SerializeField] public bool is_option_active;
    [SerializeField] Option_manager option_c;

    public bool Flugs_slide_changer;
    public bool Flugs_iscall_once;
    public bool Flugs_gaze_image;
    public bool Flugs_gaze_sw;
    public bool Flugs_continue;
    private bool Flugs_can_read_save;

    public int task_image_indx; //課題のスライド番号
    public int scene_counter;   //シーン切り替え番号, 1タイトル学生番号　2Yoni課題　3お疲れさまでした
    public int max_scene;

    public int CSV_WriteStartTime;

    public bool Can_read_save
    {
        get { return Flugs_can_read_save; }
        set { Flugs_can_read_save = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        Flugs_slide_changer = true;
        Flugs_iscall_once = false;  //初期化を行うためのフラグ
        Flugs_gaze_image = false;
        Flugs_gaze_sw = false;
        Flugs_continue = false;
        scene_counter = 1;
        max_scene = 3;
        task_image_indx = 0;
        Flugs_can_read_save = false;
        //SceneManager.LoadScene("SampleScene");
    }


    //シーン変更時の処理
    public void Change_scene()
    {
        //シーンカウンタが最大値に達したら1に戻す
        if(scene_counter == max_scene)
        {
            scene_counter = 1;
        }
        else
        {
            this.scene_counter++;
        }
        this.task_image_indx = 0;
    }

    public void Is_gaze_false()
    {
        Flugs_gaze_image = false;
    }


    public void Main_Controller()
    {
        image_c.SlideScene_Controller();    //スライドの切り替え
        button_c.Button_Controller();       //ボタンの表示非表示
        field_c.Field_Controller();         //入力フィールドの表示非表示
        gaze_c.Gaze_Controller();           //視線位置のCSV入力
        Debug.Log("Main_Controller_スライドインデックス：現在は" + this.task_image_indx + "です");
        Debug.Log($"Main_Controllerシーンカウンター：現在は{scene_counter}です");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
              UnityEngine.Application.Quit();
#endif
    }

    public void ResetFlugs()
    {
        Flugs_gaze_image = false;
        Flugs_continue = false;
        Flugs_can_read_save = false;
    }

    // Update is called once per frame

    void Update()
    {
        //1度だけ実行される初期化
        if (!Flugs_iscall_once)
        {
            Main_Controller();
            Flugs_iscall_once = true;
            Debug.Log($"初期化が実行されました");
            Debug.Log("task_indexの中身は　=　" + task_image_indx);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escapeボタンが押されました");

            option_c.Option_switch();
        }
    }
}




        /*
        switch (scene_counter)
        {
            case 1:
                if (Flugs_slide_changer)
                {
                    image_c.IDslide_Controller();
                    image_c.SlideScene_Controller();
                    button_c.Button_Controller();
                    Flugs_slide_changer = false;
                Debug.Log($"シーンカウンター：現在は{scene_counter}です");
                }
                break;

            case 2:
                if (Flugs_slide_changer)
                {
                    Debug.Log("Flugが有効になりました");
                    image_c.Taskslide_Controller();
                    button_c.Button_Controller();
                    Flugs_slide_changer = false;
                    Debug.Log("Flugが無効になりました");
                    Debug.Log("シーンカウンター：現在は２です");
                }

                break;
            default:
                Debug.Log("シーンカウンターの値が不正です");
                break;

        }
        */


