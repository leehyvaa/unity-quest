using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEx : MonoBehaviour
{
    [Range(0, 50)]
    public float distance = 10f;
    private RaycastHit rayHit;
    private Ray ray; //어느 방향으로 쏠지에 대한 정보를 가짐

    private RaycastHit[] rayHits;


    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray();
        ray.origin = transform.position; //레이의 시작점
        ray.direction = transform.forward;

        //레이를 쏠때 플레이어 자신으 오브젝트가 겹치는경우도 생기기 때문에 일정거리 앞에서
        //쏘도록 설정해놓는 경우도 많음


        //아래와 같이 정의할때 포지션과 방향을 동시에 정의 가능
        //ray = new Ray(transform.position, transform.forward);

    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position; 
        ray.direction = transform.forward;

        Ray_1();
        
    }

    //기즈모는 일반적으로 게임창에선 안보이고 에디터에서 확인하는 용도
    //업데이트에서 쓸 수도 있지만 업데이트에 넣으면 성능을 더 소모하기 때문에
    //기즈모 포함 디버그 관련한건 업데이트에 넣지 말 것
    //OnGui?같은 애들도 위와 같다
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(ray.origin, ray.direction*distance, Color.red);
        Gizmos.DrawLine(ray.origin, ray.direction * distance + ray.origin);

     

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(ray.origin, 0.1f);


        if(rayHits != null)
        {

            //충돌위치에다 기즈모 원 생성
            for (int i = 0; i < rayHits.Length; i++)
            {
                // : draw point
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(rayHits[i].point, 0.2f);

                // : draw line
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, transform.forward * rayHits[i].distance + ray.origin);


                
                // : normal vector
                //충돌체가 맞은 위치로부터 날아온 레이 방향으로 노말벡터 선이 그려짐
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
        //충돌체 하나만 검출하는 함수
        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, distance))
        {
            Debug.Log(rayHit.collider.gameObject.name);
            Debug.Log(Vector3.Distance(ray.origin, rayHit.point));
        }
    }

     void Ray_2()
    {
        //레이 거리안에 모든 충돌체를 체크하는 함수
        //레이캐스트 오버로드엔 레이어마스크 등으로 특정 물체만 검출하는 등의 오버로드가 존재
        rayHits = Physics.RaycastAll(ray, distance);

        

        for (int i = 0; i < rayHits.Length; i++)
        {
            Debug.Log(rayHits[i].collider.gameObject.name + " hit!!");
        }
    }

    void Ray_3()
    {
        //오버랩은 일정범위 (폭탄이나 범위공격) 안의 오브젝트들을 검출한다
        //스페어캐스트는 원이 쭉 이동하면서 원통형으로 지나감
        //오브젝트 속도가 너무 빠르면 콜라이더가 충돌체크를 못할 때가 있는데
        //이때 오버랩이나 스페어캐스트 등이 유용함
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
            //레이어가 박스인 오브젝트만 출력
            if (rayHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Box"))
                Debug.Log(rayHits[i].collider.gameObject.name + " hit!! - Layer");

            //태그가 스페어인 오브젝트만 출력
            if (rayHits[i].collider.gameObject.tag == "Sphere")
                Debug.Log(rayHits[i].collider.gameObject.name + " hit!! - tag");

        }
    }
}
