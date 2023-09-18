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
        //forward �� z�� ������ dirToTarget ���⺤�͸� �ٶ󺸰Բ� �Ͽ� �۵�
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.forward = dirToTarget.normalized;

    }

    void LookAt_2()
    {
        //Ÿ���� ��ġ�� ������ �۵��� �ű�� �߰��� ������ �־��� �� ����
        //Vector3 dirToTarget = target.transform.position - this.transform.position;
        //transform.LookAt(dirToTarget,Vector3.up);
        transform.LookAt(target.transform, Vector3.up);
    }

    void LookAt_3()
    {
        //Ÿ���� ���⺤�͸� �ְ�, �������� �־ �۵�
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
    }
}
