using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_manager : MonoBehaviour
{
    [SerializeField] task_manager task_c;
    [SerializeField] game_director director_c;
    [SerializeField] private TextMeshProUGUI display_text;
    int[] indx_correction; //image_indexを文字表示のindexに使用するための補正値 
    // Start is called before the first frame update
    void Start()
    {
        //追加したTEXTの表示テスト
        display_text.text = "学生番号を入力してください";
        indx_correction = new int[] { 0, 5, 100 };
    }

    // Update is called once per frame
    void Update()
    {
        if (director_c.scene_counter == 3)
        {
            display_text.text = "";
        }
        else if (director_c.scene_counter == 1 && director_c.task_image_indx == 5)
        {
            if(director_c.Can_read_save)
            {
                display_text.text = "中断データが見つかりました。ここから再開しますか？\n"+"年月日：" + task_c.Read_CSV_Date() + "\n" + "最後の問題：" + task_c.Read_CSV_Last();
            }
            else
            {
                display_text.text = "中断データが見つかりませんでした。最初からやり直しますか？";

            }
        }
        else if (director_c.Flugs_gaze_image)
        {
            if (task_c.read_Answer[director_c.task_image_indx] != "0")
            {
                display_text.text = "Yoniをクリックしてください。";
            }
            else//この部分がないと前の問題文が表示される。謎。
            {
                display_text.text = "";
            }
        }
        else
        {
            // Debug.Log("CSVから読み込む番号は"+director_c.task_image_indx +"+"+ indx_correction[director_c.scene_counter - 1]);
            display_text.text = task_c.read_Text[director_c.task_image_indx + indx_correction[director_c.scene_counter-1]];

        }
    }
}
