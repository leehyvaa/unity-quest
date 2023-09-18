using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAt_2();

    }

    void LookAt_1()
    {
        //forward 즉 z축 방향이 dirToTarget 방향벡터를 바라보게끔 하여 작동
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.forward = dirToTarget.normalized;

    }

    void LookAt_2()
    {
        //타겟의 위치를 넣으면 작동함 거기다 추가로 기준점 넣어줄 수 있음
        //Vector3 dirToTarget = target.transform.position - this.transform.position;
        //transform.LookAt(dirToTarget,Vector3.up);
        transform.LookAt(target.transform, Vector3.up);
    }

    void LookAt_3()
    {
        //타겟의 방향벡터를 넣고, 기준점을 넣어서 작동
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
    }
}
