using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [Range(0, 1)]
    public float posWeight = 1;

    [Range(0, 1)]
    public float rotWeight = 1;

   // public Transform rightHandfollowObj;

    protected Animator animator;
    private int selectWeight = 1;

    [Range(0, 359)]
    public float xRot = 0.0f;
    [Range(0, 359)]
    public float yRot = 0.0f;
    [Range(0, 359)]
    public float zRot = 0.0f;


    public Transform rightHand;
    public Transform leftHand;
    public Transform rightLeg;
    public Transform leftLeg;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        if (animator)
        {
            SetHandWeight();
            SetLegWeight();
        }
    }

    //오른팔이 오브젝트를 따라다님
    


    //1,2번을 합친것
    //1번에다가 Weight에 따라 손목을 회전시키는 움직임이 들어감
    private void SetHandWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);

        Quaternion rightHandRotation = Quaternion.LookRotation(rightHand.position - transform.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandRotation);



        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);

        Quaternion leftHandRotation = Quaternion.LookRotation(leftHand.position - transform.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);
    }

    private void SetRotationAngle()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);

        Quaternion handRotation = Quaternion.Euler(xRot, yRot, zRot);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
    }

    //오브젝트를 따라 고개가 돌아감
    private void SetLookAtObj()
    {
        //animator.SetLookAtWeight(1.0f);
        //animator.SetLookAtPosition(rightHandfollowObj.position);
    }


    private void SetLegWeight()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightLeg.position);

        Quaternion rightFootRotation = Quaternion.Euler(xRot, yRot, zRot);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);



        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, posWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, rotWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftLeg.position);

        Quaternion leftFootRotation = Quaternion.Euler(xRot, yRot, zRot);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
    }


}
