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

    public int task_image_indx; //�ۑ�̃X���C�h�ԍ�
    public int scene_counter;   //�V�[���؂�ւ��ԍ�, 1�^�C�g���w���ԍ��@2Yoni�ۑ�@3����ꂳ�܂ł���
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
        Flugs_iscall_once = false;  //���������s�����߂̃t���O
        Flugs_gaze_image = false;
        Flugs_gaze_sw = false;
        Flugs_continue = false;
        scene_counter = 1;
        max_scene = 3;
        task_image_indx = 0;
        Flugs_can_read_save = false;
        //SceneManager.LoadScene("SampleScene");
    }


    //�V�[���ύX���̏���
    public void Change_scene()
    {
        //�V�[���J�E���^���ő�l�ɒB������1�ɖ߂�
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
        image_c.SlideScene_Controller();    //�X���C�h�̐؂�ւ�
        button_c.Button_Controller();       //�{�^���̕\����\��
        field_c.Field_Controller();         //���̓t�B�[���h�̕\����\��
        gaze_c.Gaze_Controller();           //�����ʒu��CSV����
        Debug.Log("Main_Controller_�X���C�h�C���f�b�N�X�F���݂�" + this.task_image_indx + "�ł�");
        Debug.Log($"Main_Controller�V�[���J�E���^�[�F���݂�{scene_counter}�ł�");
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
        //1�x�������s����鏉����
        if (!Flugs_iscall_once)
        {
            Main_Controller();
            Flugs_iscall_once = true;
            Debug.Log($"�����������s����܂���");
            Debug.Log("task_index�̒��g�́@=�@" + task_image_indx);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape�{�^����������܂���");

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
                Debug.Log($"�V�[���J�E���^�[�F���݂�{scene_counter}�ł�");
                }
                break;

            case 2:
                if (Flugs_slide_changer)
                {
                    Debug.Log("Flug���L���ɂȂ�܂���");
                    image_c.Taskslide_Controller();
                    button_c.Button_Controller();
                    Flugs_slide_changer = false;
                    Debug.Log("Flug�������ɂȂ�܂���");
                    Debug.Log("�V�[���J�E���^�[�F���݂͂Q�ł�");
                }

                break;
            default:
                Debug.Log("�V�[���J�E���^�[�̒l���s���ł�");
                break;

        }
        */


