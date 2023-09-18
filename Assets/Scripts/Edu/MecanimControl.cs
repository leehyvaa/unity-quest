using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MecanimControl : MonoBehaviour
{


    enum MoveSpeedSetting
    {
        attackingMoveSpeed = 1,
        fightMoveSpeed = 6,
        defaultMoveSpeed= 10,

    }

    //�̵�,ȸ�� ���� �� ����
    public float moveSpeed = (float)MoveSpeedSetting.defaultMoveSpeed;
    private float tempMoveSpeed;


    public float runningCoef = 2f;


    public float rotationSpeed = 720.0f;


    //���� üũ
    bool damage = false;
    public bool isFighting = false;
    public bool isRunning = false;
    public bool isAttacking = false;


    Vector3 moveInput;


    //���۳�Ʈ��
    public WeaponManager weaponmanager;
    public Transform characterBody;
    public Transform cameraArm;
    CharacterController pcController;
    Animator animator;


    public PlayerState pState = PlayerState.P_Idle;
    public PlayerState pStatetemp;
    public float aimCooltime;
    public float aimCount;
    // Start is called before the first frame update
    void Start()
    {

        InitData();
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Input_Animation();
        CharacterController_Slerp();
    }

    private void LateUpdate()
    {

    }

    public void SetState(PlayerState state)
    {
        pState = state;
        animator.SetInteger("pState", (int)state);
    }



    void Input_Animation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("Upperbody.Attack"))
                animator.SetTrigger("Attack");
        }


        switch (pState)
        {
            case PlayerState.P_Idle:
                if (moveInput.sqrMagnitude > 0.01f)
                    SetState(PlayerState.P_Walk);


                break;
            case PlayerState.P_Walk:
                if (moveInput.sqrMagnitude == 0f)
                    SetState(PlayerState.P_Idle);
                //if (isMove&&isRunning)
                //    SetState(PlayerState.P_Run);

                break;
            case PlayerState.P_Attack:
                if (!isAttacking)
                    SetState(pStatetemp);
                else
                {
                    if (pStatetemp == pState)
                        SetState(PlayerState.P_Idle);
                    else
                        SetState(pStatetemp);

                }

                break;
            case PlayerState.P_Roll:

                break;
            case PlayerState.P_Jump:
                //if (isGround)
                //{
                //    ani.SetBool("pJumping", false);
                //    if (pStatetemp == pState)
                //        SetState(PlayerState.P_Walk);
                //    else
                //        SetState(pStatetemp);
                //    isJumping = false;

                //}
                break;
            case PlayerState.P_ChangeWeapon:

                moveSpeed = 0.5f;
                if (!WeaponManager.ChangingWeapon)
                {
                    moveSpeed = tempMoveSpeed;
                    SetState(PlayerState.P_Idle);
                }
                break;
            case PlayerState.P_Aiming:
                characterBody.forward = cameraArm.right;
                animator.SetFloat("xDir", moveInput.x);
                animator.SetFloat("yDir", moveInput.z);
                break;
        }

    }

    private void CharacterController_Slerp()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 forword = Vector3.Slerp(transform.forward, moveInput, rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, moveInput));
            transform.LookAt(transform.position + forword);
        }
        else
        {

        }

        animator.SetFloat("Speed", pcController.velocity.magnitude);
        pcController.Move(moveInput * moveSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            damage = true;
            animator.SetBool("Damage", damage);
            StartCoroutine("AttackBow");
        }
    }

    IEnumerator Die()
    {
        while (true)
        {
            yield return new WaitForSeconds(0);
            if (damage && animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Damage"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                {
                    damage = false;
                    animator.SetBool("Damage", damage);
                    break;
                }
            }
        }
    }

    private void FightingStance()
    {
        float fightingToggle;
        if ((int)WeaponManager.weaponType != 0)
        {


            isFighting = true;

            fightingToggle = 1;

            if (moveSpeed == (float)MoveSpeedSetting.fightMoveSpeed)
                moveSpeed = (float)MoveSpeedSetting.fightMoveSpeed;
            // if ((int)WeaponManager.weaponType == 2 && (int)fightingToggle == 1)
            //   characterBody.rotation = Quaternion.Euler(characterBody.rotation.x, characterBody.rotation.y + 45f, characterBody.rotation.z);
        }
        else
        {
            fightingToggle = 0;
            if (moveSpeed == (float)MoveSpeedSetting.fightMoveSpeed)
                moveSpeed = (float)MoveSpeedSetting.fightMoveSpeed;
        }

        animator.SetFloat("FightingBlend", fightingToggle);

    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            animator.SetFloat("RunBlend", 1);
        }
        else
        {
            isRunning = false;
            animator.SetFloat("RunBlend", 0);
        }
    }






    // ���콺 ��Ʈ�� �¿���
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
    //ī�޶� ������
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

    private void InitData()
    {
        weaponmanager = GetComponentInChildren<WeaponManager>();

        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        SetState(PlayerState.P_Idle);

    }
    public enum PlayerState
    {
        P_Idle,
        P_Walk,
        P_Attack,
        P_Roll,
        P_Jump,
        P_ChangeWeapon,
        P_Aiming,
    }

}



#region
/*
 *
 *  ������ �ɰ� Ư�� �ִϸ��̼��� �۵���Ű�� ���ؼ��� any State�� ����ϸ� ��
 *  Greater ũ�ų� Less �۰ų�
 *  Has Exit Time�� Idle�� �ִϸ��̼��� ���������� �����ϰ� �Ѵ�
 *  Rig Animation Type �޸շ��̵�� ��� Generic�� �������� ���� �پ��� ��
 *  ��Ų ���� materials���� textures Materials�� �������
 *  Ʈ���Ŵ� ������� ��
 *  
 */
#endregion