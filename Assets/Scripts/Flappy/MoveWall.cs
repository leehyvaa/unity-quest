using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    


    

    // Start is called before the first frame update
    void Start()
    {
        //Invoke ���� �ð��� ������ �߻��ϴ� �̺�Ʈ
        //(������Ʈ�� ��Ȱ�� �Ǿ �۵��Ѵ�)
        //(�ݴ�� �ڷ�ƾ�� ������Ʈ�� ��Ȱ���Ǹ� �۵����� ����)
        //���� �ð��� ������ �ش� �Լ��� �����Ѵ�.
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
