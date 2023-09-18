using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEx : MonoBehaviour
{
    [Range(0, 50)]
    public float distance = 10f;
    private RaycastHit rayHit;
    private Ray ray; //��� �������� ������ ���� ������ ����

    private RaycastHit[] rayHits;


    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray();
        ray.origin = transform.position; //������ ������
        ray.direction = transform.forward;

        //���̸� �� �÷��̾� �ڽ��� ������Ʈ�� ��ġ�°�쵵 ����� ������ �����Ÿ� �տ���
        //��� �����س��� ��쵵 ����


        //�Ʒ��� ���� �����Ҷ� �����ǰ� ������ ���ÿ� ���� ����
        //ray = new Ray(transform.position, transform.forward);

    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position; 
        ray.direction = transform.forward;

        Ray_1();
        
    }

    //������ �Ϲ������� ����â���� �Ⱥ��̰� �����Ϳ��� Ȯ���ϴ� �뵵
    //������Ʈ���� �� ���� ������ ������Ʈ�� ������ ������ �� �Ҹ��ϱ� ������
    //����� ���� ����� �����Ѱ� ������Ʈ�� ���� �� ��
    //OnGui?���� �ֵ鵵 ���� ����
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(ray.origin, ray.direction*distance, Color.red);
        Gizmos.DrawLine(ray.origin, ray.direction * distance + ray.origin);

     

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(ray.origin, 0.1f);


        if(rayHits != null)
        {

            //�浹��ġ���� ����� �� ����
            for (int i = 0; i < rayHits.Length; i++)
            {
                // : draw point
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(rayHits[i].point, 0.2f);

                // : draw line
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, transform.forward * rayHits[i].distance + ray.origin);


                
                // : normal vector
                //�浹ü�� ���� ��ġ�κ��� ���ƿ� ���� �������� �븻���� ���� �׷���
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(rayHits[i].point, rayHits[i].point + rayHits[i].normal);
            
                // : reflection vector
                Gizmos.color = new Color(1f, 0f, 1f);
                Vector3 reflect = Vector3.Reflect(transform.forward, rayHits[i].normal);
                Gizmos.DrawLine(rayHits[i].point, rayHits[i].point + reflect);
            }


        }
    }



    void Ray_1()
    {
        //�浹ü �ϳ��� �����ϴ� �Լ�
        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, distance))
        {
            Debug.Log(rayHit.collider.gameObject.name);
            Debug.Log(Vector3.Distance(ray.origin, rayHit.point));
        }
    }

     void Ray_2()
    {
        //���� �Ÿ��ȿ� ��� �浹ü�� üũ�ϴ� �Լ�
        //����ĳ��Ʈ �����ε忣 ���̾��ũ ������ Ư�� ��ü�� �����ϴ� ���� �����ε尡 ����
        rayHits = Physics.RaycastAll(ray, distance);

        

        for (int i = 0; i < rayHits.Length; i++)
        {
            Debug.Log(rayHits[i].collider.gameObject.name + " hit!!");
        }
    }

    void Ray_3()
    {
        //�������� �������� (��ź�̳� ��������) ���� ������Ʈ���� �����Ѵ�
        //�����ĳ��Ʈ�� ���� �� �̵��ϸ鼭 ���������� ������
        //������Ʈ �ӵ��� �ʹ� ������ �ݶ��̴��� �浹üũ�� ���� ���� �ִµ�
        //�̶� �������̳� �����ĳ��Ʈ ���� ������
        rayHits = Physics.SphereCastAll(ray, 2.0f,distance);
        string objName = "";
        foreach (RaycastHit hit in rayHits)
        {
            objName += hit.collider.name + " , ";
        }
        print(objName);
    }

    void Ray_4()
    {
        rayHits = Physics.RaycastAll(ray, distance);



        for (int i = 0; i < rayHits.Length; i++)
        {
            //���̾ �ڽ��� ������Ʈ�� ���
            if (rayHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Box"))
                Debug.Log(rayHits[i].collider.gameObject.name + " hit!! - Layer");

            //�±װ� ������� ������Ʈ�� ���
            if (rayHits[i].collider.gameObject.tag == "Sphere")
                Debug.Log(rayHits[i].collider.gameObject.name + " hit!! - tag");

        }
    }
}
