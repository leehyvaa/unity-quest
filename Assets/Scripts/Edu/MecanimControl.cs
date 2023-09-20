using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MecanimControl : MonoBehaviour
{


    public enum MoveSpeedSetting
    {
        attackingMoveSpeed = 1,
        fightMoveSpeed = 3,
        defaultMoveSpeed= 5,

    }

    //이동,회전 변수 및 배율
    public float moveSpeed = (float)MoveSpeedSetting.defaultMoveSpeed;
    public float tempMoveSpeed;


    public float runningCoef = 1.5f;


    public float rotationSpeed = 720.0f;


    //상태 체크
    bool damage = false;
    public bool isFighting = false;
    public bool isRunning = false;
    public bool isAttacking = false;


    Vector3 moveInput;
    Vector3 moveDir;

    //컴퍼넌트들
    public WeaponManager weaponmanager;
    public Transform characterBody;
    public Transform cameraArm;
    CharacterController pcController;
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip audioClip;


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
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Input_Animation();
        //CharacterController_Slerp();

        Running();
        CursorControll();
    }

    private void LateUpdate()
    {
        LookAround();
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
                PlaySound(audioClip);

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

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying) return;
        
        audioSource.PlayOneShot(clip);
        
    }

    private void StopSound()
    {
        audioSource.Stop();
    }

    private void CharacterController_Slerp()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 camForward = new Vector3(cameraArm.forward.x,0f,cameraArm.forward.z);
            Vector3 forword = Vector3.Slerp(characterBody.forward, camForward, rotationSpeed * Time.deltaTime / Vector3.Angle(characterBody.forward, moveInput));
            characterBody.LookAt(transform.position + forword);
        }
        else
        {

        }

        //animator.SetFloat("Speed", pcController.velocity.magnitude);
        //pcController.Move(moveInput * moveSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);

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
    private void Move()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (moveInput.magnitude != 0)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.z + lookRight * moveInput.x;


            Vector3 forword = Vector3.Slerp(characterBody.forward, moveDir, rotationSpeed * Time.deltaTime / Vector3.Angle(characterBody.forward, moveDir));
            characterBody.LookAt(transform.position + forword);
            
            if (isRunning)
            {

                //characterBody.forward = moveDir;

                pcController.Move(moveDir * moveSpeed * runningCoef * Time.deltaTime);
                // 스페이스바 누르면 구르기
                //if (Input.GetKeyDown(KeyCode.Space))
                //    StartCoroutine(Roll(moveDir));


            }
            else
            {
                //characterBody2.rotation = Quaternion.Euler(characterBody.rotation.x, characterBody.rotation.y + 45f, characterBody.rotation.z);

                if (pState == PlayerState.P_Aiming)
                    characterBody.forward = cameraArm.right;
                else
                    //characterBody.forward = moveDir;

                pcController.Move(moveDir * moveSpeed * Time.deltaTime);
                // 스페이스바 누르면 구르기
                //if (Input.GetKeyDown(KeyCode.Space))
                //    StartCoroutine(Roll(moveDir));



            }

            //transform.position += moveDir * Time.deltaTime * 5f;
        }
        //레이로 이동방향 확인
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

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






    // 마우스 컨트롤 온오프
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
    //카메라 움직임
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
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Sounds/foot/army");
        //audioClip = Resources.Load(string.Format("Sounds/foot/{0}", "army")) as AudioClip; 

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
 *  조건을 걸고 특정 애니메이션을 작동시키기 위해서는 any State를 사용하면 됨
 *  Greater 크거나 Less 작거나
 *  Has Exit Time은 Idle의 애니메이션이 끝나야지만 실행하게 한다
 *  Rig Animation Type 휴먼로이드는 사람 Generic은 사족보행 같은 다양한 것
 *  스킨 문제 materials에서 textures Materials를 해줘야함
 *  트리거는 비워나도 됨
 *  
 */
#endregion