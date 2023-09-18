using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Windows;

public class RacingRay : MonoBehaviour
{

    private float rotSpeed = 5f;
    private float distance = 50;
    public Transform foward;


    private Ray ray;

    private RaycastHit[] rayHits;
    private int count;
    private float[] distances = new float[18];

    private Vector3 fowardDir;

    // Start is called before the first frame update
    void Start()
    {
      
        rayHits = new RaycastHit[18];

        for (int i = 0; i < 18; i++)
        {
            distances[i] = 0;
        }

        fowardDir = Vector3.zero;
    }

    private void FixedUpdate()
    {


    }

    // Update is called once per frame
    void Update()
    {


        //���⺤��
        Vector3 dir = transform.position + Vector3.forward + Vector3.left - transform.position;

        //���⺤�͸� ���÷����̼ǿ� �����ϵ��� ��ȯ
        dir = transform.TransformDirection(dir);


        //���⺤�͸� �ٶ󺸴� ȸ�� ���ʹϾ� ����
        Quaternion q = Quaternion.LookRotation(dir);
        //���ʹϾ� i*5��ŭ up�� axis�� ȸ��
        q *= Quaternion.AngleAxis(count * 5f, Vector3.up);

        //���ʹϾ��� ���⺤�ͷ� �ٽ� ��ȯ
        Vector3 newDir = q * Vector3.forward;


        ray.origin = transform.position;
        ray.direction = newDir;

       

        CarRay();


        Vector3 newPos = transform.position + fowardDir.normalized * 10f;

        Vector3 lerpVec = Vector3.Lerp(foward.position, newPos, rotSpeed * Time.deltaTime);
        
        foward.position = lerpVec;
        
        

    }


    private void OnDrawGizmos()
    {
     
        Gizmos.DrawLine(transform.position, foward.transform.position);

        //Vector3 newPos = transform.position + fowardDir.normalized * 10f;
        //Gizmos.DrawLine(transform.position, newPos);


    }

    void CarRay()
    {



        if (Physics.Raycast(ray.origin, ray.direction, out rayHits[count], distance))
        {
            if (rayHits[count].collider.gameObject.layer == LayerMask.NameToLayer("Box"))
            {
                //Debug.Log(rayHits[count].collider.gameObject.name);
                distances[count] = Vector3.Distance(transform.position, rayHits[count].collider.transform.position);
            }
        }
        else
        {
            distances[count] = 1000f;
        }



        if (count >= 17)
        {
            float max = 0f;
            float min = 500f;
            
            int firstIndex = -1;
            int secondIndex = -1;

            int maxIndex = 9;
            int minIndex = 9;

            bool isNull = false;

            for (int i = 0; i < 18; i++)
            {
                if (distances[i] > max)
                {
                    max = distances[i];
                    maxIndex = i;

                    
                    if(max == 1000f && !isNull)
                    {
                        firstIndex = i;
                        isNull = true;
                    }

                    if(max == 1000f)
                    {
                        secondIndex = i;

                    }

                }

                if (distances[i] < min)
                {
                    min = distances[i];
                    minIndex = i;
                }

            }


            int index = 0;
            if(isNull)
            {
                index = firstIndex + (int)(secondIndex - firstIndex) ;
                //Debug.Log("N");
            }
            else
            {
                index = maxIndex;
                //Debug.Log("F");

            }



            //���⺤��
            Vector3 dir = transform.position + Vector3.forward + Vector3.left - transform.position;

            //���⺤�͸� ���÷����̼ǿ� �����ϵ��� ��ȯ
            dir = transform.TransformDirection(dir);


            //���⺤�͸� �ٶ󺸴� ȸ�� ���ʹϾ� ����
            Quaternion q = Quaternion.LookRotation(dir);


            if (distances[minIndex] < 2.5f)
            {
                if(minIndex > distances.Length/2)
                {        
                     q *= Quaternion.AngleAxis((distances.Length - minIndex) * 5f, Vector3.up);
                }
                else
                {
                     q *= Quaternion.AngleAxis((minIndex+distances.Length/2) * 5f, Vector3.up);
                }

                //Debug.Log(index);
                gameObject.GetComponent<Car>().engine -= 0.2f;
                
            }
            else
            {
                //���ʹϾ� i*5��ŭ up�� axis�� ȸ��
                q *= Quaternion.AngleAxis(index * 5f, Vector3.up);
                

            }


            // : rotation
            //Quaternion deltaRot = Quaternion.Euler(new Vector3(0, rot, 0) * rotSpeed * Time.deltaTime);
            //rb.MoveRotation(rb.rotation * deltaRot);



            //���ʹϾ��� ���⺤�ͷ� �ٽ� ��ȯ
            fowardDir = q * Vector3.forward;

            //foward.transform.position = transform.position + newDir.normalized * 10f;

            



            //car���� �ε��� ��ȯ
            count = 0;




            //for(int i = 0; i < distances.Length; i++)
            //{
            //    distances[i] = 50;
            //}
        }
        else
            count++;

    }
}
