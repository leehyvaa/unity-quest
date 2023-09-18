using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSample : MonoBehaviour
{
    public float speed = 30.0f;

    public GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        //오일러앵글로 좌표변환 - 짐벌락 현상이 있을 수 있어서
        //rotation 함수의 Quaternion을 이용
        this.transform.eulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate_1();
        //Rotate_2();
        //Rotate_3();
        //Rotate_4();
        Rotate_Around();
    }


    void Rotate_1()
    {
        //Quaternion을 이용해서 짐벌락현상을 피할 수 있음
        //월드좌표를 기준으로 해서 update에 넣어도 45도로 고정
        Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        this.transform.rotation = target;
    }
    
    void Rotate_2()
    {
        //local좌표를 기준으로 하기 때문에 update 안에 넣으면 무한으로 회전함
        //up을 기준으로 해서 45도 회전
        this.transform.Rotate(Vector3.up * 45.0f);
    }

    void Rotate_3()
    {
        //쿼터니언 사용해서 회전
        float rot = speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rot, Vector3.up);
        
    }

    void Rotate_4()
    {
        //앵글만 사용해서 회전 (다만 Rotate함수는 앵글로 넣어도 알아서 쿼터니언으로
        //변환(?)하는진 모르겠는데 짐벌락이 발생하지 않도록 처리하기
        //때문에 짐벌락이 일어나지 않음 따라서 이 함수를 자주 쓸것
        //오일러각을 사용할때는 항상 짐벌락을 주의하고
        //오일러각을 사용하려면 Rotate함수를 이용할것

        float rot = speed * Time.deltaTime;
        transform.Rotate(rot * Vector3.up);
    }

    void Rotate_Around()
    {
        //해당 점을 기준으로 회전
        float rot = speed * Time.deltaTime;
        //transform.RotateAround(new Vector3(0f, 0.5f, 0f), Vector3.up, rot);
        transform.RotateAround(target.transform.position, Vector3.up, rot);
    }
}
