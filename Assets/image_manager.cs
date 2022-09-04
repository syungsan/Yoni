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
    private Image comp_image; //�摜�R���|�\�l���g
    [SerializeField] Sprite[] enterID_images;  //�w���ԍ����͗p�̉摜
    [SerializeField] Sprite[] slide_images;  //103���̃X���C�h���X�g
    [SerializeField] Sprite gaze_image;   //�����p�̉摜 
    [SerializeField] Sprite finish_image;
    public int enter_image_length; //�w���ԍ����͗p�X���C�h�̔z��̒���
    public int task_image_length; //�ۑ�̃X���C�h�̔z��̒���



    // Start is called before the first frame update
    void Start()
    {
        comp_image = GetComponent<Image>();
        slide_images = Resources.LoadAll<Sprite>("Images/Yoni_Image");
        enterID_images = Resources.LoadAll<Sprite>("Images/Enter_ID");
        gaze_image = Resources.Load<Sprite>("Images/Others/Gaze_002");   //�����摜�͖�肩�牼�ؗp//�ʉ摜�ɕύX
        finish_image = Resources.Load<Sprite>("Images/Others/Finish_001");  //�I�����
        task_image_length = slide_images.Length;
        enter_image_length = enterID_images.Length;
        foreach (var t in slide_images)
        {
            //Debug.Log(t.name);
        }
        comp_image.color = Color.white;
        slide_number = this.slide_images.Length;
    }
    
    //�ۑ�̉摜�\��
    public void Taskslide_Controller()
    {
        //�z��O�̉摜��ǂݍ��܂Ȃ��悤�ɂ���
        if (director_c.task_image_indx == this.slide_number)
        {
            Debug.Log($"IDslide_Controller�̃C���f�b�N�X���ő�ɒB���܂���({director_c.task_image_indx}/{slide_images.Length})");
            director_c.Change_scene();
            //�ۑ�I����ʂ�\��
            write_c.WriteScore();
            Gaze_c.WriteGaze();
            this.comp_image.sprite = this.finish_image;


        }
        //Debug.Log("Taskslide_Controller()");
        //4���{�^�����������ケ�ꂩ���̎��̃X���C�h�����p�[�g�Ȃ�Β����摜���X�L�b�v
        else if (director_c.Flugs_gaze_image && task_c.read_Answer[director_c.task_image_indx] != "0" && task_c.read_Answer[director_c.task_image_indx+1] != "0")
        {
            Debug.Log($"IDslide_Controller�̃C���f�b�N�X�͍ő�l�����ł�({director_c.task_image_indx}/{slide_images.Length})");
            this.comp_image.sprite = this.gaze_image;
        }
        else
        {
            Debug.Log($"IDslide_Controller�̃C���f�b�N�X�͍ő�l�����ł�({director_c.task_image_indx}/{slide_images.Length})");
            this.comp_image.sprite = this.slide_images[director_c.task_image_indx];

        }
        
    }

    

    //�w���ԍ�����
    public void IDslide_Controller()
    {
        if (director_c.task_image_indx == this.enterID_images.Length)
        {
            Debug.Log($"IDslide_Controller�̃C���f�b�N�X���ő�ɒB���܂���({director_c.task_image_indx}/{enterID_images.Length})");
            
            director_c.Change_scene();
            //�V�[���J�E���^�[�Q�̓��e�𖳗������s
            this.Taskslide_Controller();
        }
        else
        {
            Debug.Log($"IDslide_Controller�̃C���f�b�N�X�͍ő�l�����ł�({director_c.task_image_indx}/{enterID_images.Length})");
            this.comp_image.sprite = this.enterID_images[director_c.task_image_indx];
        }
        
    }

    public void FinishSlide_Controller()
    {
        director_c.Is_gaze_false(); //�����摜�̗L���t���O��������
        director_c.Change_scene();
        IDslide_Controller();
    }


    public void SlideScene_Controller()
    {
        Debug.Log("�V�[���J�E���^�[�́�" + director_c.scene_counter);
        Debug.Log("�X���C�h�C���f�b�N�X�́�"+ director_c.task_image_indx);
        switch(director_c.scene_counter)
        {
            case 1:
                Debug.Log("�X���C�h�R���g���[���[_case1���s");
                IDslide_Controller();
                break;
            case 2:
                Taskslide_Controller();
                break;
            case 3:
                FinishSlide_Controller();
                break;
            default:
                Debug.Log("�C���[�W�R���g���[���[�F�s���ȃV�[���ł�");
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
