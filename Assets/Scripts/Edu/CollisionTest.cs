using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    float speedMove = 10f;
    float speedRotate = 100f;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }


    private void FixedUpdate()
    {
        //0.02�� ���� �ذ��� �� �ִ� �ڵ���� �־�� �ʹ� ������� �ȵ�
        //���������� ������� ���� ��

        float rot = Input.GetAxis("Horizontal");
        float mov = Input.GetAxis("Vertical");

        //������ٵ� �����ؼ� ȸ���Ϸ��� ���ʹϾ��� ����ؾ���
        
        // : rotation
        Quaternion deltaRot = Quaternion.Euler(new Vector3(0,rot, 0) * speedRotate * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRot);

        // : movement
        Vector3 move = transform.forward * mov;
        Vector3 newPos = rb.position + move * speedMove * Time.deltaTime;
        rb.MovePosition(newPos);

    }

    // Update is called once per frame
    void Update()
    {
        //float rot = Input.GetAxis("Horizontal");  
        //float mov = Input.GetAxis("Vertical");

        //rot = rot * speedRotate * Time.deltaTime;
        //mov = mov * speedRotate * Time.deltaTime;

        //gameObject.transform.Rotate(Vector3.up * rot);
        //gameObject.transform.Translate(Vector3.forward * mov);



    }

    

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        print("Collider �浹" + hitObject.name + "�� �浹 ����");
    }

    private void OnCollisionStay(Collision collision)
    {

        GameObject hitObject = collision.gameObject;
        print("Collider �浹" + hitObject.name + "�� �浹��");
    }

    private void OnCollisionExit(Collision collision)
    {

        GameObject hitObject = collision.gameObject;
        print("Collider �浹" + hitObject.name + "�� �浹����");
    }



    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger �浹" + hitObject.name + "�� �浹����");
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger �浹" + hitObject.name + "�� �浹��");
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger �浹" + hitObject.name + "�� �浹����");
    }
}
