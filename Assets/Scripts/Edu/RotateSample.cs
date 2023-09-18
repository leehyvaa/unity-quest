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
        //���Ϸ��ޱ۷� ��ǥ��ȯ - ������ ������ ���� �� �־
        //rotation �Լ��� Quaternion�� �̿�
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
        //Quaternion�� �̿��ؼ� ������������ ���� �� ����
        //������ǥ�� �������� �ؼ� update�� �־ 45���� ����
        Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        this.transform.rotation = target;
    }
    
    void Rotate_2()
    {
        //local��ǥ�� �������� �ϱ� ������ update �ȿ� ������ �������� ȸ����
        //up�� �������� �ؼ� 45�� ȸ��
        this.transform.Rotate(Vector3.up * 45.0f);
    }

    void Rotate_3()
    {
        //���ʹϾ� ����ؼ� ȸ��
        float rot = speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(rot, Vector3.up);
        
    }

    void Rotate_4()
    {
        //�ޱ۸� ����ؼ� ȸ�� (�ٸ� Rotate�Լ��� �ޱ۷� �־ �˾Ƽ� ���ʹϾ�����
        //��ȯ(?)�ϴ��� �𸣰ڴµ� �������� �߻����� �ʵ��� ó���ϱ�
        //������ �������� �Ͼ�� ���� ���� �� �Լ��� ���� ����
        //���Ϸ����� ����Ҷ��� �׻� �������� �����ϰ�
        //���Ϸ����� ����Ϸ��� Rotate�Լ��� �̿��Ұ�

        float rot = speed * Time.deltaTime;
        transform.Rotate(rot * Vector3.up);
    }

    void Rotate_Around()
    {
        //�ش� ���� �������� ȸ��
        float rot = speed * Time.deltaTime;
        //transform.RotateAround(new Vector3(0f, 0.5f, 0f), Vector3.up, rot);
        transform.RotateAround(target.transform.position, Vector3.up, rot);
    }
}
