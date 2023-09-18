using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        //월드좌표기준 5를 이동
        //this.transform.position = new Vector3 (0f, 5f, 0f);
        
        //로컬좌표기준 5를 이동 (원래 좌표에 +더한 좌표로 이동, 0,0.5,0 -> 0,5.5,0)
        this.transform.Translate(new Vector3(0f,5f,0f));
    }

    // Update is called once per frame
    void Update()
    {
        //Move_1();
        //Move_2();
        Move_Control();
    }

    void Move_1()
    {
        float moveDelta = this.moveSpeed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos.z += moveDelta;
        this.transform.position = pos;
    }

    void Move_2()
    {
        float moveDelta = this.moveSpeed * Time.deltaTime;
        this.transform.Translate(Vector3.forward * moveDelta);
    }

    void Move_Control()
    {
        float move = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime* moveSpeed * move);
    }
}
