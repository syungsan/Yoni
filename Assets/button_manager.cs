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
        Debug.Log("Button_A�̒��g�́�" + Button_A);
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

        //�{�^���̈ʒu���̎擾
        Button_decision_comp = Button_ID_decision.GetComponent<RectTransform>();

        //Enter_ID_Field = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();


        TodayNow = DateTime.Today;
        Data = TodayNow.Year.ToString() + "." + TodayNow.Month.ToString() + "." + TodayNow.Day.ToString();

        n = 0;
        x = 0.0;

    }

    //�e�{�^���̉����ꂽ�Ƃ��̏���
    public void ButtonClick(string button_name)
    {
        switch (button_name)
        {
            case "Button_A":
                //Debug.Log("�{�^��A��������܂����B");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("A");
                //Debug.Log(director_c.task_image_indx);
                break;
            case "Button_B":
                //Debug.Log("�{�^��B��������܂����B");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("B");
                break;
            case "Button_C":
                //Debug.Log("�{�^��C��������܂����B");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("C");
                break;
            case "Button_D":
                //Debug.Log("�{�^��D��������܂����B");
                director_c.Flugs_gaze_image = true;
                CheckNextSlide();
                SetStartTime();
                Judge("D");
                break;
            case "Button_All":
                //Debug.Log("�{�^��All��������܂����B");


                NonJudge();
                director_c.task_image_indx++;
                SetStartTime();
                //A-D�̑I���̂Ƃ�
                if (task_c.read_Answer[director_c.task_image_indx] != "0")
                {
                    director_c.Flugs_gaze_image = false;
                    gaze_c.Quit_Counter_Update();   //���f�J�E���^�[�����̖��ɍX�V

                }
                //Debug.Log(director_c.task_image_indx);
                break;
            case "Button_ID_decision":
                //Debug.Log("�{�^��ID_decision��������܂����B");
                field_c.InputID();
                //�w���ԍ����͉�ʂɕ���������Ίm�F��ʂɂ�����
                Button_OK_Function();
                break;
            case "Button_Yes"://Cancel
                //�ǉ������{�^������
                Debug.Log("Yes�{�^����������܂����B");
                //Debug.Log("Yes�{�^�����������Ƃ��̒��g�́@��"+field_c.ID);

                //�I�v�V������\�����Ă���Ƃ��ɃL�����Z��
                if (option_c.is_option_prop)
                {
                    option_c.Option_switch();
                }
                //�w���ԍ��̊m�F�ŃL�����Z��������
                else if (director_c.scene_counter == 1)
                {
                    //�I���m�F��Yes
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
                //�ۑ肪�I���I�����L�����Z�������ꍇ
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
                //�ۑ�I����ɂ�߂�Ƃ�
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
                        Debug.LogError("�f�B���N�g���̍폜�Ɏ��s���܂����B");
                    }
                    director_c.Quit();
                }
                //�I�v�V��������stop����Ƃ�
                else if (option_c.is_option_prop)
                {
                    //stop�m�F��Yes
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
                //�w���ԍ��̓��͊m�F��OK��������
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
                    gaze_c.Quit_Counter_Update();   //���f�J�E���^�[���X�V
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
                Debug.Log("�Q�Ƃ���Ă��܂���B");
                break;
        }

        director_c.Main_Controller();

    }


    //�S�͈͂̎��̃{�^���z�u
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

        Debug.Log("����͑S��ʃ{�^�����s���ł�");
    }

    //�^�[�Q�b�g���̃{�^���z�u
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

        Debug.Log("����͑S��ʃ{�^�����s���ł�");
    }

    //4���̎��̃{�^���z�u
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

    //�w���ԍ����͎��̃{�^��
    public void IDButton_input()
    {

        Debug.Log("Button_A�̒��g�́�" + Button_A);
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

        Debug.Log("����͊w���ԍ����͎��s���ł�");
    }

    //�w���ԍ��m�F���̃{�^���z��
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

        Debug.Log("����͊w���ԍ��m�F���ł�");
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


    //�w���ԍ�����͂������OK���������ꍇ�A�R���e�B�j���[�ɂ�镪��
    private void Is_Continue()
    {
        if (!director_c.Flugs_continue)//�ŏ�����͂��߂��ꍇ
        {
            SetStartTime(); //�f�o�b�O�p�̎��Ԏ擾
            gaze_c.Gaze_Data_Reset();

            gaze_c.GazeCsvStart();
            write_c.WriteCSV();
            director_c.task_image_indx += 2;
            n = 1;
            SetStartTime(); //��
            gaze_c.Start_Gazelist_Write();//muriyari ���ԂO�ɂ�����


        }


        else if (director_c.Can_read_save)//�R���e�B�j���[���ɒ��f�t�@�C���̓ǂݍ��݂ɐ��������ꍇ
        {

            gaze_c.ContinueTimeUpdate();//���f���Ԃ̒ǉ�
            gaze_c.Gaze_Data_Reset();   //�������X�g�Ǝ��Ԃ̏�����
            write_c.ContinueScore();
            gaze_c.ContinueGaze();
            director_c.Change_scene();
            director_c.task_image_indx = task_c.Read_CSV_Indx(); //�ĊJ���a��̖��܂ŃC���f�b�N�X���X�V����
            n = int.Parse(task_c.Read_CSV_Last()) + 1; //�����̖��ԍ����X�V����
            total = task_c.Read_CSV_LastTime();//   ���̍��v���Ԃ��X�V
            //director_c.Main_Controller();
            gaze_c.Gaze_Time_Reset();
            gaze_c.Quit_Counter_Update();   //���f�J�E���^�[�����̖��ɍX�V
            Reset_x_Update(); //x�̎��Ԃ��X�V
        }

        else if (director_c.task_image_indx == 5)//�������t�@�C�����Ȃ��ŏ����瑱�s����Ƃ�
        {
            gaze_c.Gaze_Data_Reset();
            gaze_c.GazeCsvStart();
            write_c.WriteCSV();
            director_c.task_image_indx++;
            gaze_c.Quit_Counter_Update();   //���f�J�E���^�[�����̖��ɍX�V
        }
        //������stop�Z�[�u���F���ł��Ă��邩�m�F�������I�I            

        else//�w���ԍ�����͂��t�@�C�������邩�m�F����Ƃ�
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

    private void Can_ReadGaze()//�������Ƃ�Ă��邩�ǂ���
    {
        if (gaze_c.is_enableGaze)//�������Ƃ�Ă���΃e�X�g��ʂɐi��
        {
            director_c.task_image_indx = 3;
            //gaze_image_c.GazeImage_show();
        }
        else//�������Ƃ�Ă��Ȃ���ΐ�ɐi�܂��Ȃ�
        {
            director_c.task_image_indx = 3;
        }
    }

    //�V�[����C���f�b�N�X���̃{�^���ݒ�
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
                    Debug.Log($"Button_Controller_���{director_c.task_image_indx + 1}�̓�����{task_c.read_Answer[director_c.task_image_indx]}�ł��I");
                    if (task_c.read_Answer[director_c.task_image_indx] == "0")
                    {
                        this.OneButton();
                    }
                    //���̃X���C�h�����łȂ���Β����摜���X�L�b�v
                    else if (task_c.read_Answer[director_c.task_image_indx + 1] == "0")
                    {
                        //director_c.task_image_indx++;
                        Debug.Log("Part�\�����");
                        this.FourButton();
                    }
                    else
                    {
                        if (director_c.Flugs_gaze_image)
                        {
                            Debug.Log("Button_Controller_�����摜�̃{�^����\��" + director_c.Flugs_gaze_image);
                            this.TargetButton();
                        }
                        else
                        {
                            Debug.Log("Button_Controller_4���{�^����\��" + director_c.Flugs_gaze_image);
                            this.FourButton();
                        }
                    }
                    break;
                case 3:
                    IDButton_confirmation();
                    field_c.Delete_Field();
                    break;
                default:
                    Debug.Log("�{�^���R���g���[���[�F�s���ȃV�[���ł�");
                    break;
            }
        }
    }

    //�����摜���X�L�b�v���鏈��
    public void CheckNextSlide()
    {
        if (task_c.read_Answer[director_c.task_image_indx + 1] == "0")
        {
            director_c.task_image_indx++;
            Debug.Log("Part�\����ʂ��\�������̂Œ����摜���X�L�b�v���܂���");
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
