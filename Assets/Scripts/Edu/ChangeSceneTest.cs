using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButton(0))
        //{
        //    GameManager.Instance.ChangeScene();
        //}

        if (Input.GetMouseButton(1))
        {
            if (GameManager.Instance.changeScene == 0)
            {
                GameManager.Instance.ChangeScene("99_End");
                GameManager.Instance.changeScene++;
            }
            else if (GameManager.Instance.changeScene == 1)
            {
                GameManager.Instance.ChangeScene("03_Collision");
                GameManager.Instance.changeScene++;
            }
        }

    }


    //OnGUI는 너무 느려서 게임개발에서는 보통 쓰이지 않음
    //간단한 정보출력에는 사용가능하다
    private void OnGUI()
    {
        if(GUI.Button(new Rect(100,200,200,30), "씬 변경"))
        {
            if (GameManager.Instance.changeScene == 0)
            {
                GameManager.Instance.ChangeScene("99_End");
                GameManager.Instance.changeScene++;
            }
            else if (GameManager.Instance.changeScene == 1)
            {
                GameManager.Instance.ChangeScene("03_Collision");
                GameManager.Instance.changeScene++;
            }

        }
    }
}

//레이싱 OnGUI로 레이팅 표기하고, 재시작할지 등을 표기