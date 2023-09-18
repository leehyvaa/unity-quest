using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    
    public Transform axis;
    public Transform obj;

    float propSpeed = 2000f;
    float rotSpeed = 3f;
    float propPower = 0f;

    Rigidbody rb;
    public Transform cameraArm;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateProps();
        Move();
        RotateBody();

        CursorControll();
    }

    private void LateUpdate()
    {
        LookAround();
    }

    private void FixedUpdate()
    {

        Vector3 dir = axis.position - transform.position;
        //rb.AddForce(Vector3.down * 1000f);

        if (propPower > 0)
        {
            rb.AddForce(dir.normalized * Time.deltaTime * 150 * propPower);
            //rb.velocity = dir.normalized * Time.deltaTime * 200 * propPower;
        }


        if (transform.position.y < 0)
        {
            transform.position= new Vector3(transform.position.x,0f,transform.position.z);
        }

        if(rb.velocity.y < -30f)
        {
            rb.velocity= new Vector3(rb.velocity.x,-30f,rb.velocity.z);
            Debug.Log(rb.velocity.y);
        }
        if(rb.velocity.y > 35f)
        {
            Debug.Log(rb.velocity.y);
            rb.velocity = new Vector3(rb.velocity.x, 30f, rb.velocity.z);
        }

        if (rb.velocity.x > 35f)
        {
            rb.velocity = new Vector3(30, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < -35f)
        {
            rb.velocity = new Vector3(-30, rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z > 35f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 30);
        }
        if (rb.velocity.z < -35f)
        {
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, -30);

        }


        if (transform.position.y > 180f)
        {
            transform.position = new Vector3(transform.position.x, 180f,transform.position.z);
        }

    }

    void RotateProps()
    {
        float rot = propSpeed * Time.deltaTime;
        if(propPower >0)
            axis.transform.Rotate(rot* Vector3.up * propPower*0.3f);
    }

    void Move()
    {
        Vector2 input;
        input.y = Input.GetAxis("Vertical");
        input.x = Input.GetAxis("Horizontal");




       

        if (input.magnitude > 0)
        {


            //transform.Translate(dir.normalized * Time.deltaTime * moveSpeed* input.y*3);
            obj.Rotate(rotSpeed * Time.deltaTime * Vector3.up * 20 * input.x);

        }

        if (propPower < 0)
        {
            propPower = 0f;
        }
        else if (propPower <= 5)
        {
            if (input.y == 0f)
                propPower -= 0.002f;
            else
                propPower += input.y * 0.002f;
        }
        else
        {
            propPower = 5f;
        }
       // Debug.Log(propPower);
   
    }

    void RotateBody()
    {
        if(Input.GetKey(KeyCode.Keypad8))
        {
            obj.Rotate(rotSpeed* Time.deltaTime * Vector3.right * 20);
        }
        else if(Input.GetKey(KeyCode.Keypad5))
        {
            obj.Rotate(-rotSpeed* Time.deltaTime * Vector3.right* 20);
        }
        else if(Input.GetKey(KeyCode.Keypad4))
        {
            obj.Rotate(rotSpeed * Time.deltaTime * Vector3.forward * 20);
        }
        else if(Input.GetKey(KeyCode.Keypad6))
        {
            obj.Rotate(-rotSpeed * Time.deltaTime * Vector3.forward * 20);
        }

    }



    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

    }


    private void CursorControll()
    {
        if (Input.anyKeyDown)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (!Cursor.visible && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSpeed, 0f, Space.World);
        // transform.Rotate(-Input.GetAxis("Mouse Y") * mouseSpeed, 0f, 0f);

    }
}
