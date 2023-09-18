using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    


    

    // Start is called before the first frame update
    void Start()
    {
        //Invoke 일정 시간이 지나면 발생하는 이벤트
        //(오브젝트가 비활성 되어도 작동한다)
        //(반대로 코루틴은 오브젝트가 비활성되면 작동하지 않음)
        //일정 시간이 지나면 해당 함수를 실행한다.
        Invoke("Death", 3f);

    }

    

    // Update is called once per frame
    void Update()
    {
        if (FlappyManager.Instance.gameState == FlappyManager.GameState.Playing)
        {
            transform.Translate(Vector3.left * Time.deltaTime* moveSpeed);
            
        }
        

    }

    void Death()
    {
        DestroyImmediate(this.gameObject);
    }
}
