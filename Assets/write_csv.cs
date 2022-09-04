using System.IO;
using System.Text;
using UnityEngine;
using System;

// csv�ɕۑ����邽�߂̃R�[�h
// SaveCsv�փA�^�b�`
public class write_csv : MonoBehaviour
{
    [SerializeField] game_director director_c;
    [SerializeField] inputfield_manager field_c;
    [SerializeField] task_manager task_c;
    //[SerializeField] button_manager button_c;
    // System.IO
    [SerializeField] private StreamWriter sw;
    [SerializeField] private StreamWriter sw_continue;
    private StreamWriter quick_sw;
    DateTime Now;
    public string data;



    public void Start()
    {
        Now = DateTime.Now;
        data = Now.Year.ToString() + "." + Now.Month.ToString() + "." + Now.Day.ToString() + "_" + Now.Hour.ToString() + "h" + Now.Minute.ToString() + "m" + Now.Second.ToString() + "s";
        Debug.Log("���ݎ�����" + data);
    }


    // Start is called before the first frame
    public void WriteCSV()
    {
        string path1 = @"Score\" + field_c.ID + "_" + data + ".file";
        string path2 = @"Stop\" + field_c.ID + ".file";

        Directory.CreateDirectory(path1);
        Directory.CreateDirectory(path2);

        Debug.Log("�t�H���_���쐬����܂���");

        // �V����csv�t�@�C�����쐬���āA{}�̒��̗v�fcsv�ɒǋL����
        sw = new StreamWriter(@path1 + "\\" + data + "_" + field_c.ID + "." + "Score.csv", false/*, Encoding.GetEncoding("Shift_JIS")*/);

        Debug.Log("Score��CSV�t�@�C�����쐬����܂���");

        // csv1�s�ڂ̃J�����ŁAStreamWriter �I�u�W�F�N�g�֏�������
        string[] s1 = { "Data", "ID", "Number", "Answer", "TF", "Response", "Total", "StartTime", "EndTime", "Slide" };

        // s1�̕����z��̂��ׂĂ̗v�f���u, �v�ŘA������
        string s2 = string.Join(",", s1);

        // s2�������csv�t�@�C���֏�������
        sw.WriteLine(s2);

        quick_sw = new StreamWriter(@path2 + "\\QuickSave_Score" + "_" + field_c.ID + ".csv", false/*, Encoding.GetEncoding("Shift_JIS")*/);
        string[] s3 = { "Data", "ID", "Number", "Answer", "TF", "Response", "Total", "StartTime", "EndTime", "Slide" };
        string s4 = string.Join(",", s3);
        quick_sw.WriteLine(s4);

    }

    // Update is called once per frame
    /*
    void Update()
    {
        // Enter�L�[�������ꂽ��csv�t�@�C���ւ̏������݂��I������
        if (Input.GetKeyDown(KeyCode.Return))
        {
          
        }
    }*/

    public void SaveData(string txt1, string txt2, int txt3, string txt4, string txt5, double txt6, double txt7, int txt8, int txt9, int txt10)
    {
        string[] s1 = { txt1, txt2, txt3.ToString(), txt4, txt5, txt6.ToString(), txt7.ToString(), txt8.ToString(), txt9.ToString(), txt10.ToString()};
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);

        string[] s3 = { txt1, txt2, txt3.ToString(), txt4, txt5, txt6.ToString(), txt7.ToString(), txt8.ToString(), txt9.ToString(), txt10.ToString()};
        string s4 = string.Join(",", s3);
        quick_sw.WriteLine(s4);
    }

    public void WriteScore()
    {
        sw.Close();
        Debug.Log("Score���������܂ꂽ");
    }

    //stop����ꍇ��CSV�����o��
    public void QuickSave_Score()
    {
        quick_sw.Close();
        Debug.Log("�r��Score���������܂ꂽ");
    }

    public void ContinueScore()
    {
        string path1 = @"Score\" + field_c.ID + "_" + data + ".file";
        string path2 = @"Stop\" + field_c.ID + ".file";

        Directory.CreateDirectory(path1);
        Directory.CreateDirectory(path2);

        sw = new StreamWriter(@path1 + "\\" + data + "_" + field_c.ID + "." + "Score.csv", false/*, Encoding.GetEncoding("UTF-8")*/);
        quick_sw = new StreamWriter(@path2 + "\\QuickSave_Score" + "_" + field_c.ID + ".csv", true/*, Encoding.GetEncoding("UTF-8")*/);

        task_c.quick_score_Data.ForEach(gpl =>
        {
            sw.WriteLine(string.Join(",",gpl));
        });
    }
}
