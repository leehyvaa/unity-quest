using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Car : MonoBehaviour
{
    public Transform car;
    public Transform foward;
    public Transform body;
    public Transform[] wheels;
    public Transform cameraArm;
    
    

    public Vector2 input;
    public bool auto;
    public bool go;


    public int lap = -1;
    private float moveSpeed;
    private Rigidbody rb;
    public int rank;
    public string lapTime;

    public Color color;

    public float engine;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        moveSpeed = 30f;
        input = Vector2.zero;
        rb = car.GetComponent<Rigidbody>();
        rank = 0;
        engine = 0;
    }


    protected void FixedUpdate()
    {
        if (!go)
            return;
        MoveCar();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RotateCar();


    }



    protected void LateUpdate()
    {
        if(cameraArm != null)
            LookAround();
    }
    void MoveCar()
    {
        
        //자율주행
        if(!auto)
            input.y = Input.GetAxis("Vertical");
        





        float rotSpeed = 4f * Time.deltaTime;

        if(input.y != 0)
        {
            Vector3 targetDir = (foward.transform.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, targetRotation, rotSpeed);




            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].transform.Rotate(-input.y * rotSpeed * 100 * Vector3.up);
            }

            if(engine <=1f)
            {
                engine += input.y * 0.04f;
            }
            else
            {
                engine = 1f;
            }
        }


        Vector3 dir = foward.transform.position - transform.position;
        dir.Normalize();
        //transform.Translate(dir * Time.deltaTime * moveSpeed * input.y);



        
        // : movement
        Vector3 move = dir * engine;
        Vector3 newPos = rb.position + move * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPos);

    }

    void RotateCar()
    {
        input.x = Input.GetAxis("Horizontal");
        float rotSpeed = 70f * Time.deltaTime;


        if(!auto)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                foward.RotateAround(transform.position, Vector3.up, input.x * rotSpeed*2);
            }
            else
            {
                foward.RotateAround(transform.position, Vector3.up, input.x* rotSpeed);
            }

        }

        




        Vector3 dir = foward.transform.position - this.transform.position;
        dir = new Vector3(dir.x,dir.y,dir.z);

        for (int i = 0; i < wheels.Length / 2; i++)
        {
            wheels[i].rotation = Quaternion.LookRotation(dir, Vector3.left);

            //Vector3 targetDir = (foward.transform.position - transform.position).normalized;
            //Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            //Vector3 newDir = new Vector3(wheels[i].localEulerAngles.x, targetRotation.eulerAngles.y, 90);

            //wheels[i].rotation = Quaternion.LookRotation(targetDir, -body.right);
            


            //wheels[i].rotation = Quaternion.Slerp(wheels[i].transform.rotation, Quaternion.Euler(newDir), rotSpeed);

            //Debug.Log(wheels[i].localRotation.x);
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


}
