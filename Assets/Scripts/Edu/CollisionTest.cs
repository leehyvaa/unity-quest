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
        //0.02초 내에 해결할 수 있는 코드들을 넣어라 너무 길어지면 안됨
        //물리관련한 내용등을 넣을 것

        float rot = Input.GetAxis("Horizontal");
        float mov = Input.GetAxis("Vertical");

        //리지드바디 관련해서 회전하려면 쿼터니언을 사용해야함
        
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
        print("Collider 충돌" + hitObject.name + "와 충돌 시작");
    }

    private void OnCollisionStay(Collision collision)
    {

        GameObject hitObject = collision.gameObject;
        print("Collider 충돌" + hitObject.name + "와 충돌중");
    }

    private void OnCollisionExit(Collision collision)
    {

        GameObject hitObject = collision.gameObject;
        print("Collider 충돌" + hitObject.name + "와 충돌종료");
    }



    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger 충돌" + hitObject.name + "와 충돌시작");
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger 충돌" + hitObject.name + "와 충돌중");
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("Trigger 충돌" + hitObject.name + "와 충돌종료");
    }
}
