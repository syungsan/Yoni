using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using System;
using System.Text;
using System.IO;

public class GetGazePosition : MonoBehaviour
{
    [SerializeField] inputfield_manager field_c;
    [SerializeField] game_director director_c;
    [SerializeField] task_manager task_c;
    [SerializeField] button_manager button_c;
    [SerializeField] write_csv write_c;

    GameObject warning_NoGaze;
    RectTransform canvas_size;
    private int starttime;
    private int now;
    public int duration;
    public int continue_time;
    private int listCount;
    private StreamWriter sw;
    private StreamWriter sw_continue;
    //private bool onceWrite;
    private Vector2 roundedSampleInput;
    private Vector2 original_position;
    private Vector2 gazePosition;
    private float nofound_counter;
    public bool is_enableGaze;

    private List<string> gaze_position_list;

    GazePoint gazePoint;
    DateTime Now;
    //string data;
    public int quit_counter;//中断時削除する部分のカウンター

    // Start is called before the first frame update
    void Start()
    {
        gazePoint = TobiiAPI.GetGazePoint();
        canvas_size = GameObject.Find("Canvas").GetComponent<RectTransform>();
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        gaze_position_list = new List<string>();
        //onceWrite = true;
        Now = DateTime.Now;
        //data = Now.Year.ToString() + "." + Now.Month.ToString() + "." + Now.Day.ToString() + "_" + Now.Hour.ToString() + "h" + Now.Minute.ToString() + "m" + Now.Second.ToString() + "s";
        nofound_counter = 0.0f;
        warning_NoGaze = GameObject.Find("Image_NoGaze");
        warning_NoGaze.SetActive(false);
        is_enableGaze = false; //視線が認識できているかの初期化
        continue_time = 0;  //中断追加時間を0にして最初に説いたときに影響が出ないようなする
        quit_counter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        gazePoint = TobiiAPI.GetGazePoint();


        //if文後から前に移動させました
        now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        duration = now - starttime + continue_time;


        if (gazePoint.IsValid)
        {
            gazePosition = gazePoint.Screen;
            roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
            string[] gp1 = { duration.ToString(), roundedSampleInput.x.ToString(), roundedSampleInput.y.ToString(),"True", director_c.task_image_indx.ToString() , director_c.Flugs_gaze_image.ToString(), director_c.CSV_WriteStartTime.ToString()};
            string gp2 = string.Join(";", gp1);
            //Yoni課題中のみ書き込むやつ　臨時
            //if(director_c.scene_counter == 2)
            {
                gaze_position_list.Add(gp2);
            }
            nofound_counter = 0;

            //Debug.Log("x (in px): " + roundedSampleInput.x);
            //Debug.Log("y (in px): " + roundedSampleInput.y);
        }
        else
        {
            //Debug.Log("out of screen");
            string[] gp1 = { duration.ToString(), "null", "null", "False", director_c.task_image_indx.ToString(), director_c.Flugs_gaze_image.ToString() , director_c.CSV_WriteStartTime.ToString() }; //Input.mousePosition.x.ToString()};
            string gp2 = string.Join(";", gp1);
            //if (director_c.scene_counter == 2)
            {
                gaze_position_list.Add(gp2);
            }

            nofound_counter += Time.deltaTime;
            //Debug.Log("視線が計測されていない時間は：" + nofound_counter);
        }


        //視線が認識できない場合表示
        if(nofound_counter > 3.0f)
        {
            warning_NoGaze.SetActive(true);
            is_enableGaze = false;
        }
        else
        {
            warning_NoGaze.SetActive(false);
            is_enableGaze = true;
        }

        // Debug.Log(roundedSampleInput.x);
        //Debug.Log(duration);

        /*if (Input.GetKeyDown(KeyCode.Return) && onceWrite)
        {
            gaze_position_list.ForEach(gpl =>
            {
                sw.WriteLine(gpl);
            });

            
            sw.Close();
            onceWrite = false;
        }*/
    }

    public int Click_time()
    {
        listCount = gaze_position_list.Count;
        Debug.Log("要素数は＝" + listCount);
        return (duration);
    }

    public int Reset_Click_time()
    {
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        listCount = gaze_position_list.Count;
        Debug.Log("要素数は＝" + listCount);
        return (duration);
    }

    public Vector2 View_Position()
    {
        return gazePosition;
    }

    //中断時のリストのインデックスのやつを更新
    public void Quit_Counter_Update()
    {
        quit_counter = gaze_position_list.Count - 2;
    }

    public void GazeCsvStart()
    {
        string path1 = @"Score\" + field_c.ID + "_" + write_c.data + ".file";

        Directory.CreateDirectory(path1);

        sw = new StreamWriter(@path1 + "\\" + write_c.data + "_" + field_c.ID + "." + "Gaze.csv", false/*, Encoding.GetEncoding("Shift_JIS")*/);
        gaze_position_list.Add("list_array[task_number:patterncount],Screen_X,Screen_Y,,date,data_Length,player_name,total_distance,include_count");
    }

    public void ContinueGaze()
    {
        string path1 = @"Score\" + field_c.ID + "_" + write_c.data + ".file";

        Directory.CreateDirectory(path1);

        sw = new StreamWriter(@path1 + "\\" + write_c.data + "_" + field_c.ID + "." + "Gaze.csv", false/*, Encoding.GetEncoding("Shift_JIS")*/);

        gaze_position_list = task_c.quick_gaze_Data;

        task_c.quick_gaze_Data.ForEach(gpl =>
        {
            sw.WriteLine(gpl);
        });

    }

    public void Gaze_Controller()
    {
        switch (director_c.scene_counter)
        {
            case 1:
                /*if(director_c.task_image_indx == 1 && director_c.Flugs_gaze_sw)
                {
                    GazeCsvStart();
                    director_c.Flugs_gaze_sw = false;
                }
                */
                break;

            case 2:
                break;
            case 3:
                /*gaze_position_list.ForEach(gpl =>
                {
                    sw.WriteLine(gpl);


                sw.Close();
                Debug.Log("CSVの書き込みが終了しました");*/
                break;
            default:
                break;
        }
    }
    public void WriteGaze()
    {
        string[] gaze_data = { "0", canvas_size.sizeDelta.x.ToString(), canvas_size.sizeDelta.y.ToString(), "", write_c.data, (gaze_position_list.Count - 2).ToString(), field_c.ID, "0", "0" };
        string gaze_data_join = string.Join("," ,gaze_data);
        gaze_position_list.Insert(1, gaze_data_join);
        gaze_position_list.ForEach(gpl =>
        {
            sw.WriteLine(gpl);
        });
        sw.Close();
        Debug.Log("視線が書き込まれた");
    }

    //視線データのリセットと時間の更新
    public void Gaze_Data_Reset()
    {
        gaze_position_list.Clear();
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;

    }

    public void Gaze_Time_Reset()
    {
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
    }


    public void QuickSave_Gaze()
    {
        quit_Delete_list();
        string path2 = @"Stop\" + field_c.ID  + ".file";

        StreamWriter quick_sw = new StreamWriter(@path2 + "\\QuickSave_Gaze" + "_" + field_c.ID  + ".csv", false/*, Encoding.GetEncoding("Shift_JIS")*/);
        gaze_position_list.ForEach(gpl =>
        {
            quick_sw.WriteLine(gpl);
        });
        quick_sw.Close();
        Debug.Log("視線が書き込まれた");
    }

    public void ContinueTimeUpdate()
    {
        this.continue_time = task_c.Read_CSV_ContinueTime();
    }

    private void quit_Delete_list()
    {
        if (!director_c.Flugs_gaze_image)
        {
            gaze_position_list.RemoveRange(quit_counter, gaze_position_list.Count - quit_counter);
        }
    }

    public void Start_Gazelist_Write()
    {
        button_c.SetStartTime();

        if (gazePoint.IsValid)
        {
            gazePosition = gazePoint.Screen;
            roundedSampleInput = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
            string[] gp1 = { "0", roundedSampleInput.x.ToString(), roundedSampleInput.y.ToString(), "True", director_c.task_image_indx.ToString(), director_c.Flugs_gaze_image.ToString(), director_c.CSV_WriteStartTime.ToString() };
            string gp2 = string.Join(";", gp1);
            //Yoni課題中のみ書き込むやつ　臨時
                gaze_position_list.Add(gp2);
            //Debug.Log("x (in px): " + roundedSampleInput.x);
            //Debug.Log("y (in px): " + roundedSampleInput.y);
        }
        else
        {
            //Debug.Log("out of screen");
            string[] gp1 = { "0", "null", "null", "False", director_c.task_image_indx.ToString(), director_c.Flugs_gaze_image.ToString(), director_c.CSV_WriteStartTime.ToString() }; //Input.mousePosition.x.ToString()};
            string gp2 = string.Join(";", gp1);
                gaze_position_list.Add(gp2);
            //Debug.Log("視線が計測されていない時間は：" + nofound_counter);
        }
    }

}
