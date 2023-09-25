using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    [Range(0, 1)]
    public float posWeight = 1;

    [Range(0, 1)]
    public float rotWeight = 1;

    public Transform rightHandfollowObj;

    protected Animator animator;
    private int selectWeight = 1;

    [Range(0, 359)]
    public float xRot = 0.0f;
    [Range(0, 359)]
    public float yRot = 0.0f;
    [Range(0, 359)]
    public float zRot = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            selectWeight = 1; // :position

        if (Input.GetKeyDown(KeyCode.Alpha2))
            selectWeight = 2; // :rotation
       
        if (Input.GetKeyDown(KeyCode.Alpha3))
            selectWeight = 3; // 
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            selectWeight = 4; // 

        if (Input.GetKeyDown(KeyCode.Alpha5))
            selectWeight = 5; // 

        if (Input.GetKeyDown(KeyCode.Alpha6))
            selectWeight = 6; // 
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(animator)
        {
            if(rightHandfollowObj != null)
            {
                switch (selectWeight)
                {
                    case 1: SetPositionWeight(); break;
                    case 2: SetRotationWeight();  break;
                    case 3: SetEachWeight();  break;
                    case 4: SetRotationAngle(); break;
                    case 5: SetLookAtObj(); break;
                    case 6: SetLegWeight(); break;

                }

            }
        }
    }

    //오른팔이 오브젝트를 따라다님
    private void SetPositionWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandfollowObj.position);
        
        Quaternion handRotation = Quaternion.LookRotation(rightHandfollowObj.position - transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
        
    }

    //Weight에 따라 오브젝트를 향해 손목만 회전
    private void SetRotationWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandfollowObj.position);

        Quaternion handRotation = Quaternion.LookRotation(rightHandfollowObj.position - transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
    }

    //1,2번을 합친것
    //1번에다가 Weight에 따라 손목을 회전시키는 움직임이 들어감
    private void SetEachWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandfollowObj.position);

        Quaternion handRotation = Quaternion.LookRotation(rightHandfollowObj.position - transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
    }

    private void SetRotationAngle()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandfollowObj.position);

        Quaternion handRotation = Quaternion.Euler(xRot,yRot, zRot);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
    }

    //오브젝트를 따라 고개가 돌아감
    private void SetLookAtObj()
    {
        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(rightHandfollowObj.position);
    }


    private void SetLegWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightHandfollowObj.position);

        Quaternion handRotation = Quaternion.Euler(xRot, yRot, zRot);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, handRotation);
    }
}
