using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class task_manager : MonoBehaviour
{ 
    [SerializeField] game_director director_c;
    [SerializeField] inputfield_manager field_c;
    [SerializeField] TextAsset csv_answer;
   
    TextAsset csv_text;
    TextAsset csv_quick_score;
    TextAsset csv_quick_gaze;
    string answer_pass;
    [SerializeField] List<string> answer_Data = new List<string>();
    [SerializeField] List<string> text_Data = new List<string>();
    public List<string[]> quick_score_Data = new List<string[]>();
    public List<string> quick_gaze_Data = new List<string>();


    public List<string> read_Answer
    {
        get
        {
            return answer_Data;
        }
    }
    public List<string> read_Text
    {
        get { return text_Data; }
    }

    // Start is called before the first frame update
    void Start()
    {
        answer_pass = "CSV/answer";
        csv_answer = Resources.Load(answer_pass) as TextAsset;
        StringReader reader = new StringReader(csv_answer.text);
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            answer_Data.Add(line);
        }
        Debug.Log(answer_Data[1]);
        csv_text = Resources.Load("CSV/question_text_tmp") as TextAsset;
        reader = new StringReader(csv_text.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            text_Data.Add(line);
        }
    }


    //クイックセーブの読み込み「未完成」
    private void Read_QuickSave()
    {
        string score_path = "Stop/" + field_c.ID + ".file" + "/QuickSave_Score" + "_" + field_c.ID+".csv";
        Debug.Log(score_path);
        using (StreamReader streamReader = new StreamReader(score_path, Encoding.UTF8))
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                quick_score_Data.Add(line.Split(','));
            }
        /*
                csv_quick_score = Resources.Load(score_path) as TextAsset;
        StringReader reader = new StringReader(csv_quick_score.text);
        while(reader.Peek() != -1)
        {

        }*/

        string gaze_path = "Stop/" + field_c.ID + ".file" + "/QuickSave_Gaze" + "_" + field_c.ID+".csv";
        //csv_quick_gaze = Resources.Load("Stop/" + field_c.ID + ".file" + "/QuickSave_Gaze" + "_" + field_c.ID) as TextAsset;
        using (StreamReader streamReader = new StreamReader(gaze_path, Encoding.UTF8))
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                quick_gaze_Data.Add(line);
            }


        /*reader = new StringReader(csv_quick_gaze.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            quick_gaze_Data.Add(line);
        }*/
    }

    public void Can_Read_QuickSave()
    {
        string score_path = @"\Stop\" + field_c.ID + ".file" + "\\QuickSave_Score" + "_" + field_c.ID;//Assets/Resources/stop/n22m306.file/QuickSave_Score_n22m306.csv
        string debug_path = @"Stop\" + field_c.ID + @".file\QuickSave_Score_" + field_c.ID + ".csv";
        Debug.Log("ファイルの有無：" + System.IO.File.Exists(debug_path));
        //score_path = "Resources/Images/Yoni_Image";
        //Assets\Resources\stop\n22m306.file
        //Assets/Resources/stop/n22m306.file/QuickSave_Score_n22m306.csv
        Debug.Log(debug_path);
        if(System.IO.File.Exists(debug_path))
        {
            Read_QuickSave();
            Debug.Log("ファイルが見つかりました"+ quick_score_Data.Count);
            if (quick_score_Data.Count > 2)
            {
                director_c.Can_read_save = true;
                Debug.Log("データが存在しています");
            }
            else
            {
                Debug.Log("データがないです");
                director_c.Can_read_save = false;
            }
        }
        else
        {
            Debug.Log("ファイルが見つかりませんでした。");
            director_c.Can_read_save = false;
        }
    }




    public string Read_CSV_Date()
    {
        return quick_score_Data[quick_score_Data.Count - 1][0];
    }
    public string Read_CSV_Last()
    {
        return quick_score_Data[quick_score_Data.Count - 1][2];
    }

    public int Read_CSV_Indx()
    {
        int indx = int.Parse(quick_score_Data[quick_score_Data.Count - 1][9]);
        return indx + 1;
    }

    public double Read_CSV_LastTime()
    {
        double time = double.Parse(quick_score_Data[quick_score_Data.Count - 1][6]);
        return time + 1;
    }

    public int Read_CSV_ContinueTime()
    {

        Debug.Log("最後の時間の値は＝　" + quick_gaze_Data.Count);
        string before_cut = quick_gaze_Data[quick_gaze_Data.Count - 1];
        string[] after_cut = before_cut.Split(";");
        //int continue_time = int.Parse(quick_score_Data[quick_score_Data.Count - 1][8]);
        int continue_time = int.Parse(after_cut[0]);
        Debug.Log("最後の時間の値は＝　" + continue_time);
        return continue_time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
