using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class button_manager : MonoBehaviour
{
    [SerializeField] image_manager image_c;
    [SerializeField] game_director director_c;
    [SerializeField] task_manager task_c;
    [SerializeField] inputfield_manager field_c;
    [SerializeField] write_csv write_c;
    [SerializeField] GetGazePosition gaze_c;
    [SerializeField] Option_manager option_c;
    [SerializeField] TextMeshProUGUI decision_text;
    [SerializeField] gazeposition_image_manager gaze_image_c;
    GameObject Button_A;
    GameObject Button_B;
    GameObject Button_C;
    GameObject Button_D;
    GameObject Button_All;
    GameObject Button_ID_decision;
    GameObject Button_Yes;
    GameObject Button_No;
    GameObject Button_Target;
    GameObject Button_Stop;
    GameObject Button_Continue;
    RectTransform Button_decision_comp;
    //TMP_InputField Enter_ID_Field;
    DateTime TodayNow;


    string Data;
    DateTime Now;

    double x, response, total;
    int n;


    public void Start()
    {
        Button_A = GameObject.Find("Button_A");
        Debug.Log("Button_Aの中身は＝" + Button_A);
        Button_B = GameObject.Find("Button_B");
        Button_C = GameObject.Find("Button_C");
        Button_D = GameObject.Find("Button_D");
        Button_All = GameObject.Find("Button_All");
        Button_ID_decision = GameObject.Find("Button_ID_dicision");
        Button_Yes = GameObject.Find("Button_Yes");
        Button_No = GameObject.Find("Button_No");
        Button_Target = GameObject.Find("Button_Target");
        Button_Stop = GameObject.Find("Button_Stop");
        Button_Continue = GameObject.Find("Button_Continue");

        //ボタンの位置情報の取得
        Button_decision_comp = Button_ID_decision.GetComponent<RectTransform>();

        //Enter_ID_Field = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();


        TodayNow = DateTime.Today;
        Data = TodayNow.Year.ToString() + "." + TodayNow.Month.ToString() + "." + TodayNow.Day.ToString();

        n = 0;
        x = 0.0;

    }

    //各ボタンの押されたときの処理
    public void ButtonClick(string button_name)
    {
        switch (button_name)
        {
            case "Button_A":
                //Debug.Log("ボタンAが押されました。");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("A");
                //Debug.Log(director_c.task_image_indx);
                break;
            case "Button_B":
                //Debug.Log("ボタンBが押されました。");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("B");
                break;
            case "Button_C":
                //Debug.Log("ボタンCが押されました。");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("C");
                break;
            case "Button_D":
                //Debug.Log("ボタンDが押されました。");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("D");
                break;
            case "Button_All":
                //Debug.Log("ボタンAllが押されました。");


                NonJudge();
                director_c.task_image_indx++;
                SetStartTime();
                //A-Dの選択のとき
                if (task_c.read_Answer[director_c.task_image_indx] != "0")
                {
                    director_c.Flugs_gaze_image = false;
                    gaze_c.Quit_Counter_Update();   //中断カウンターを今の問題に更新

                }
                //Debug.Log(director_c.task_image_indx);
                break;
            case "Button_ID_decision":
                //Debug.Log("ボタンID_decisionが押されました。");
                field_c.InputID();
                //学生番号入力画面に文字があれば確認画面にすすむ
                Button_OK_Function();
                break;
            case "Button_Yes"://Cancel
                //追加したボタン処理
                Debug.Log("Yesボタンが押されました。");
                //Debug.Log("Yesボタンを押したときの中身は　＝"+field_c.ID);

                //オプションを表示しているときにキャンセル
                if (option_c.is_option_prop)
                {
                    option_c.Option_switch();
                }
                //学生番号の確認でキャンセルした時
                else if (director_c.scene_counter == 1)
                {
                    //終了確認のYes
                    //gaze_c.GazeCsvStart();
                    //write_c.WriteCSV();
                    //File.Delete(@"QuiclSave_Score" + "." + field_c.ID + ".csv");
                    if (director_c.task_image_indx == 5)
                    {
                        director_c.task_image_indx -= 2;

                    }
                    else
                    {
                        director_c.task_image_indx--;

                    }
                }
                //課題が終わり終了をキャンセルした場合
                else
                {
                    gaze_c.QuickSave_Gaze();
                    write_c.QuickSave_Score();
                    director_c.task_image_indx++;
                    Directory.Delete(@"Stop\" + field_c.ID + ".file", true);
                    director_c.ResetFlugs();
                }
                break;
            case "Button_No"://OK
                //課題終了後にやめるとき
                if (director_c.scene_counter == 3)
                {
                    gaze_c.QuickSave_Gaze();
                    write_c.QuickSave_Score();
                    try
                    {

                        DeleteDirectory();

                    }
                    catch
                    {
                        Debug.LogError("ディレクトリの削除に失敗しました。");
                    }
                    director_c.Quit();
                }
                //オプションからstopするとき
                else if (option_c.is_option_prop)
                {
                    //stop確認のYes
                    if (director_c.scene_counter == 1)
                    {
                        director_c.Quit();
                    }
                    else
                    {
                        write_c.WriteScore();
                        gaze_c.WriteGaze();
                        Directory.Delete(@"Score\" + field_c.ID + "_" + write_c.data + ".file", true);
                        gaze_c.QuickSave_Gaze();
                        write_c.QuickSave_Score();
                        director_c.Quit();
                    }
                }
                //学生番号の入力確認でOK押した時
                else
                {
                    SetStartTime();
                    Is_Continue();

                }
                break;
            case "Button_Target":
                x_Update();
                director_c.task_image_indx++;
                Debug.Log(gaze_c.Click_time());
                if (task_c.read_Answer[director_c.task_image_indx] != "0")
                {
                    director_c.Flugs_gaze_image = false;
                    gaze_c.Quit_Counter_Update();   //中断カウンターを更新
                }
                break;
            case "Button_Stop":
                //option_c.Option_switch();
                break;
            case "Button_Continue":
                Can_ReadGaze();
                director_c.Flugs_continue = true;
                break;
            default:
                Debug.Log("参照されていません。");
                break;
        }

        director_c.Main_Controller();

    }


    //全範囲の時のボタン配置
    public void OneButton()
    {
        this.Button_A.SetActive(false);
        this.Button_B.SetActive(false);
        this.Button_C.SetActive(false);
        this.Button_D.SetActive(false);
        this.Button_All.SetActive(true);
        this.Button_ID_decision.SetActive(false);
        this.Button_Yes.SetActive(false);
        this.Button_No.SetActive(false);
        this.Button_Target.SetActive(false);
        this.Button_Continue.SetActive(false);

        Debug.Log("これは全画面ボタン実行中です");
    }

    //ターゲット時のボタン配置
    public void TargetButton()
    {
        this.Button_A.SetActive(false);
        this.Button_B.SetActive(false);
        this.Button_C.SetActive(false);
        this.Button_D.SetActive(false);
        this.Button_All.SetActive(false);
        this.Button_ID_decision.SetActive(false);
        this.Button_Yes.SetActive(false);
        this.Button_No.SetActive(false);
        this.Button_Target.SetActive(true);
        this.Button_Continue.SetActive(false);

        Debug.Log("これは全画面ボタン実行中です");
    }

    //4択の時のボタン配置
    public void FourButton()
    {
        this.Button_A.SetActive(true);
        this.Button_B.SetActive(true);
        this.Button_C.SetActive(true);
        this.Button_D.SetActive(true);
        this.Button_All.SetActive(false);
        this.Button_ID_decision.SetActive(false);
        this.Button_Yes.SetActive(false);
        this.Button_No.SetActive(false);
        this.Button_Target.SetActive(false);
        this.Button_Continue.SetActive(false);

    }

    //学生番号入力時のボタン
    public void IDButton_input()
    {

        Debug.Log("Button_Aの中身は＝" + Button_A);
        this.Button_A.SetActive(false);
        this.Button_B.SetActive(false);
        this.Button_C.SetActive(false);
        this.Button_D.SetActive(false);
        this.Button_All.SetActive(false);
        this.Button_ID_decision.SetActive(true);
        this.Button_Yes.SetActive(false);
        this.Button_No.SetActive(false);
        this.Button_Target.SetActive(false);
        this.Button_Continue.SetActive(false);

        Debug.Log("これは学生番号入力実行中です");
    }

    //学生番号確認時のボタン配列
    public void IDButton_confirmation()
    {
        this.Button_A.SetActive(false);
        this.Button_B.SetActive(false);
        this.Button_C.SetActive(false);
        this.Button_D.SetActive(false);
        this.Button_All.SetActive(false);
        this.Button_ID_decision.SetActive(false);
        this.Button_Yes.SetActive(true);
        this.Button_No.SetActive(true);
        this.Button_Target.SetActive(false);
        this.Button_Continue.SetActive(false);

        Debug.Log("これは学生番号確認中です");
    }

    public void TitleButton()
    {
        this.Button_A.SetActive(false);
        this.Button_B.SetActive(false);
        this.Button_C.SetActive(false);
        this.Button_D.SetActive(false);
        this.Button_All.SetActive(false);
        this.Button_ID_decision.SetActive(true);
        this.Button_Yes.SetActive(false);
        this.Button_No.SetActive(false);
        this.Button_Target.SetActive(false);
        this.Button_Continue.SetActive(true);
    }

    public void Button_decision_Position()
    {
        Vector2 Button_Start_position = new Vector2(0f, -80.0f);
        Vector2 Button_OK_position = new Vector2(0f, -50.0f);

        if (director_c.task_image_indx == 0)
        {
            Button_decision_comp.anchoredPosition = Button_Start_position;
            decision_text.text = "Start";
        }
        else
        {
            Button_decision_comp.anchoredPosition = Button_OK_position;
            decision_text.text = "OK";
        }
    }


    //学生番号を入力した後にOKを押した場合、コンティニューによる分岐
    private void Is_Continue()
    {
        if (!director_c.Flugs_continue)//最初からはじめた場合
        {
            SetStartTime(); //デバッグ用の時間取得
            gaze_c.Gaze_Data_Reset();

            gaze_c.GazeCsvStart();
            write_c.WriteCSV();
            director_c.task_image_indx += 2;
            n = 1;
            SetStartTime(); //ん
            gaze_c.Start_Gazelist_Write();//muriyari 時間０にしたい


        }


        else if (director_c.Can_read_save)//コンティニュー時に中断ファイルの読み込みに成功した場合
        {

            gaze_c.ContinueTimeUpdate();//中断時間の追加
            gaze_c.Gaze_Data_Reset();   //視線リストと時間の初期化
            write_c.ContinueScore();
            gaze_c.ContinueGaze();
            director_c.Change_scene();
            director_c.task_image_indx = task_c.Read_CSV_Indx(); //再開時斬回の問題までインデックスを更新する
            n = int.Parse(task_c.Read_CSV_Last()) + 1; //続きの問題番号を更新する
            total = task_c.Read_CSV_LastTime();//   問題の合計時間を更新
            //director_c.Main_Controller();
            gaze_c.Gaze_Time_Reset();
            gaze_c.Quit_Counter_Update();   //中断カウンターを今の問題に更新
            Reset_x_Update(); //xの時間を更新
        }

        else if (director_c.task_image_indx == 5)//正しいファイルがなく最初から続行するとき
        {
            gaze_c.Gaze_Data_Reset();
            gaze_c.GazeCsvStart();
            write_c.WriteCSV();
            director_c.task_image_indx++;
            gaze_c.Quit_Counter_Update();   //中断カウンターを今の問題に更新
        }
        //ここでstopセーブが認識できているか確認したい！！            

        else//学生番号を入力しファイルがあるか確認するとき
        {
            task_c.Can_Read_QuickSave();
            director_c.task_image_indx++;
        }
    }

    private void Button_OK_Function()
    {
        if (field_c.ID != "")
        {
            director_c.task_image_indx++;
        }
        else if (director_c.task_image_indx == 0)
        {
            Can_ReadGaze();
        }
        else if (director_c.task_image_indx == 1)
        {
            director_c.task_image_indx += 2;
            gaze_image_c.GazeImage_hide();
        }
        else if (director_c.task_image_indx == 2)
        {
            //quit_gamesitai
            director_c.task_image_indx -= 2;

        }
        else
        {
            //director_c.task_image_indx++;

        }
    }

    private void Can_ReadGaze()//視線がとれているかどうか
    {
        if (gaze_c.is_enableGaze)//視線がとれていればテスト画面に進む
        {
            director_c.task_image_indx = 3;
            //gaze_image_c.GazeImage_show();
        }
        else//視線がとれていなければ先に進ませない
        {
            director_c.task_image_indx = 3;
        }
    }

    //シーンやインデックス毎のボタン設定
    public void Button_Controller()
    {
        if (option_c.is_option_prop)
        {
            IDButton_confirmation();
        }
        else
        {
            switch (director_c.scene_counter)
            {
                case 1:
                    Button_decision_Position();
                    if (director_c.task_image_indx == 0)
                    {
                        TitleButton();
                    }
                    else if (director_c.task_image_indx < 4)
                    {
                        IDButton_input();
                    }
                    else
                    {
                        IDButton_confirmation();
                    }

                    break;
                case 2:
                    Debug.Log($"Button_Controller_問題{director_c.task_image_indx + 1}の答えは{task_c.read_Answer[director_c.task_image_indx]}です！");
                    if (task_c.read_Answer[director_c.task_image_indx] == "0")
                    {
                        this.OneButton();
                    }
                    //次のスライドが問題でなければ注視画像をスキップ
                    else if (task_c.read_Answer[director_c.task_image_indx + 1] == "0")
                    {
                        //director_c.task_image_indx++;
                        Debug.Log("Part表示画面");
                        this.FourButton();
                    }
                    else
                    {
                        if (director_c.Flugs_gaze_image)
                        {
                            Debug.Log("Button_Controller_注視画像のボタンを表示" + director_c.Flugs_gaze_image);
                            this.TargetButton();
                        }
                        else
                        {
                            Debug.Log("Button_Controller_4択ボタンを表示" + director_c.Flugs_gaze_image);
                            this.FourButton();
                        }
                    }
                    break;
                case 3:
                    IDButton_confirmation();
                    field_c.Delete_Field();
                    break;
                default:
                    Debug.Log("ボタンコントローラー：不明なシーンです");
                    break;
            }
        }
    }

    //注視画像をスキップする処理
    public void CheckNextSlide()
    {
        if (task_c.read_Answer[director_c.task_image_indx + 1] == "0")
        {
            director_c.task_image_indx++;
            Debug.Log("Part表示画面が表示されるので注視画像をスキップしました");
        }
    }



    public void SetStartTime()
    {
        director_c.CSV_WriteStartTime = gaze_c.Click_time();
    }

    public void Judge(string TF)
    {

        response = (gaze_c.Click_time() - x) / 1000;

        total += response;

        if (task_c.read_Answer[director_c.task_image_indx] == TF)
        {
            write_c.SaveData(Data, field_c.ID, n, TF, "T", response, total, (int)x, gaze_c.duration, director_c.task_image_indx);
        }
        else
        {
            write_c.SaveData(Data, field_c.ID, n, TF, "F", response, total, (int)x, gaze_c.duration, director_c.task_image_indx);
        }

        x = gaze_c.Click_time();

        n++;
    }

    public void NonJudge()
    {
        response = (gaze_c.Click_time() - x) / 1000;

        total += response;

        write_c.SaveData(Data, field_c.ID, n, "", "", response, total, (int)x, gaze_c.duration, director_c.task_image_indx);

        x = gaze_c.Click_time();
    }

    public void x_Update()
    {
        x = gaze_c.Click_time();
    }

    public void Reset_x_Update()
    {
        x = gaze_c.Reset_Click_time();
    }

    private void DeleteDirectory()
    {
        Directory.Delete(@"Stop\" + field_c.ID + ".file", true);
    }


}
