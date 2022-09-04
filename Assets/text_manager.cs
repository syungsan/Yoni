using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_manager : MonoBehaviour
{
    [SerializeField] task_manager task_c;
    [SerializeField] game_director director_c;
    [SerializeField] private TextMeshProUGUI display_text;
    int[] indx_correction; //image_index�𕶎��\����index�Ɏg�p���邽�߂̕␳�l 
    // Start is called before the first frame update
    void Start()
    {
        //�ǉ�����TEXT�̕\���e�X�g
        display_text.text = "�w���ԍ�����͂��Ă�������";
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
                display_text.text = "���f�f�[�^��������܂����B��������ĊJ���܂����H\n"+"�N�����F" + task_c.Read_CSV_Date() + "\n" + "�Ō�̖��F" + task_c.Read_CSV_Last();
            }
            else
            {
                display_text.text = "���f�f�[�^��������܂���ł����B�ŏ������蒼���܂����H";

            }
        }
        else if (director_c.Flugs_gaze_image)
        {
            if (task_c.read_Answer[director_c.task_image_indx] != "0")
            {
                display_text.text = "Yoni���N���b�N���Ă��������B";
            }
            else//���̕������Ȃ��ƑO�̖�蕶���\�������B��B
            {
                display_text.text = "";
            }
        }
        else
        {
            // Debug.Log("CSV����ǂݍ��ޔԍ���"+director_c.task_image_indx +"+"+ indx_correction[director_c.scene_counter - 1]);
            display_text.text = task_c.read_Text[director_c.task_image_indx + indx_correction[director_c.scene_counter-1]];

        }
    }
}
