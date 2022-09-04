using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image_manager : MonoBehaviour
{
    [SerializeField] button_manager button_c;
    [SerializeField] game_director director_c;
    [SerializeField] task_manager task_c;
    [SerializeField] GetGazePosition Gaze_c;
    [SerializeField] Option_manager opiton_c;
    [SerializeField] write_csv write_c;
    [SerializeField] int slide_number;
    private Image comp_image; //画像コンポ―ネント
    [SerializeField] Sprite[] enterID_images;  //学生番号入力用の画像
    [SerializeField] Sprite[] slide_images;  //103枚のスライドリスト
    [SerializeField] Sprite gaze_image;   //注視用の画像 
    [SerializeField] Sprite finish_image;
    public int enter_image_length; //学生番号入力用スライドの配列の長さ
    public int task_image_length; //課題のスライドの配列の長さ



    // Start is called before the first frame update
    void Start()
    {
        comp_image = GetComponent<Image>();
        slide_images = Resources.LoadAll<Sprite>("Images/Yoni_Image");
        enterID_images = Resources.LoadAll<Sprite>("Images/Enter_ID");
        gaze_image = Resources.Load<Sprite>("Images/Others/Gaze_002");   //注視画像は問題から仮借用//別画像に変更
        finish_image = Resources.Load<Sprite>("Images/Others/Finish_001");  //終了画面
        task_image_length = slide_images.Length;
        enter_image_length = enterID_images.Length;
        foreach (var t in slide_images)
        {
            //Debug.Log(t.name);
        }
        comp_image.color = Color.white;
        slide_number = this.slide_images.Length;
    }
    
    //課題の画像表示
    public void Taskslide_Controller()
    {
        //配列外の画像を読み込まないようにする
        if (director_c.task_image_indx == this.slide_number)
        {
            Debug.Log($"IDslide_Controllerのインデックスが最大に達しました({director_c.task_image_indx}/{slide_images.Length})");
            director_c.Change_scene();
            //課題終了画面を表示
            write_c.WriteScore();
            Gaze_c.WriteGaze();
            this.comp_image.sprite = this.finish_image;


        }
        //Debug.Log("Taskslide_Controller()");
        //4択ボタンを押した後これかこの次のスライドが次パートならば注視画像をスキップ
        else if (director_c.Flugs_gaze_image && task_c.read_Answer[director_c.task_image_indx] != "0" && task_c.read_Answer[director_c.task_image_indx+1] != "0")
        {
            Debug.Log($"IDslide_Controllerのインデックスは最大値未満です({director_c.task_image_indx}/{slide_images.Length})");
            this.comp_image.sprite = this.gaze_image;
        }
        else
        {
            Debug.Log($"IDslide_Controllerのインデックスは最大値未満です({director_c.task_image_indx}/{slide_images.Length})");
            this.comp_image.sprite = this.slide_images[director_c.task_image_indx];

        }
        
    }

    

    //学生番号入力
    public void IDslide_Controller()
    {
        if (director_c.task_image_indx == this.enterID_images.Length)
        {
            Debug.Log($"IDslide_Controllerのインデックスが最大に達しました({director_c.task_image_indx}/{enterID_images.Length})");
            
            director_c.Change_scene();
            //シーンカウンター２の内容を無理やり実行
            this.Taskslide_Controller();
        }
        else
        {
            Debug.Log($"IDslide_Controllerのインデックスは最大値未満です({director_c.task_image_indx}/{enterID_images.Length})");
            this.comp_image.sprite = this.enterID_images[director_c.task_image_indx];
        }
        
    }

    public void FinishSlide_Controller()
    {
        director_c.Is_gaze_false(); //注視画像の有無フラグを初期化
        director_c.Change_scene();
        IDslide_Controller();
    }


    public void SlideScene_Controller()
    {
        Debug.Log("シーンカウンターは＝" + director_c.scene_counter);
        Debug.Log("スライドインデックスは＝"+ director_c.task_image_indx);
        switch(director_c.scene_counter)
        {
            case 1:
                Debug.Log("スライドコントローラー_case1実行");
                IDslide_Controller();
                break;
            case 2:
                Taskslide_Controller();
                break;
            case 3:
                FinishSlide_Controller();
                break;
            default:
                Debug.Log("イメージコントローラー：不明なシーンです");
                break;

        }
    }

    public void Slide_Change_Color()
    {
        /*
        if (opiton_c.is_option_prop)
        {
            comp_image.color = Color.gray;
        }
        else
        {
            comp_image.color = Color.white;
        }*/

    }
    /*
    void Update()
    {
    }*/
}
