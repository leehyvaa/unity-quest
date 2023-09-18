using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public delegate void Callback_CreateMonster(int id);
    Callback_CreateMonster setState;

    //상태 3개 맨손 검 보우 / 키1,2,3으로 나눔
    //왼손 오른손 포지션 지정
    //애니메이션에서 이벤트로 장착,해제하게끔 함수 작성
    //


    //활 장착 혹은 비장착 포지션, 웨폰슬룻
    public Transform bowEquipPos;
    public Transform bowUnEquipPos;
    public Transform weaponSloot;

    //무기 장착 위치 (왼손과 오른손)
    public Transform leftHand;
    public Transform rightHand;

    //무기 트랜스폼
    public Transform sword;
    public Transform bow;

    //무기 장착상태 
    public bool isEquipWeapon = false;

    //public static Transform currentWeapon; //현재 무기
    //public static Animator currentWeaponAnim; //현재 무기의 애니메이션

    //무기 교체 중복 실행 방지
    public static bool ChangingWeapon = false;
    //무기 바꿀때 딜레이
    public float weaponSwitchDelay = 1.15f;

    public static WeaponType weaponType; //현재 무기

    public WeaponType weaponTypeTemp; // 무기에서 맨손으로 전환할때 사용하는 변수 예를들어 맨손에서 1-x 혹은 2-x 입력
    public WeaponType weaponTypeTemp2; // 무기에서 다른무기로 전환할때 사용하는 변수 1-2 혹은 2-1 입력 
    
    //public MecanimControl movement;


    public GameObject Owner { get; set; }

    //인풋
    //private PlayerInputHandler playerInput;

    //private SwordController theSwordController; 검을 들었을때 SwordController.cs 활성화
    //private BowController theSwordController; 활을 들었을때 BowController.cs 활성화
    private void Start()
    {
        movement = GetComponentInParent<MecanimControl>();
        weaponType = WeaponType.Hand;
        isEquipWeapon = false;
        bow.position = bowUnEquipPos.position;
    }

    private void Update()
    {
        //맨손
        if (Input.GetKeyDown(KeyCode.X) && (weaponType !=WeaponType.Hand))
        {
            if (movement.pState != MecanimControl.PlayerState.P_Idle)
                return;

            movement.tempMoveSpeed = movement.moveSpeed;
            weaponTypeTemp = weaponType;
            StartCoroutine(UnEquipWeaponChanging());
        }

        //칼뽑기
        if (Input.GetKeyDown(KeyCode.Alpha1) && (weaponType != WeaponType.Sword))
        {
            if (movement.pState != MecanimControl.PlayerState.P_Idle)
                return;

            movement.tempMoveSpeed = movement.moveSpeed;
            weaponTypeTemp = weaponType;
            weaponType = WeaponType.Sword;
            weaponTypeTemp2 = weaponType;

            StartCoroutine(WeaponToWeaponChange());
            

        }

        //활뽑기
        if (Input.GetKeyDown(KeyCode.Alpha2) && (weaponType!=WeaponType.Bow))
        {
            if (movement.pState != MecanimControl.PlayerState.P_Idle)
                return;

            movement.tempMoveSpeed = movement.moveSpeed;
            weaponTypeTemp = weaponType;
            weaponType = WeaponType.Bow;
            weaponTypeTemp2 = weaponType;

            StartCoroutine(WeaponToWeaponChange());
            

        }

        
    }

   
    private void FixedUpdate()
    {
        //애니메이션 변수 업데이트
        movement.ani.SetInteger("wState", (int)weaponType);
        movement.ani.SetInteger("wTemp", (int)weaponTypeTemp);

        //movement.ani.SetBool("wState",ChangingWeapon);
        movement.ani.SetBool("isEquipWeapon", isEquipWeapon);
        movement.ani.SetFloat("wTypeBlend", (float)(weaponType-1));
    }

    //무기에서 다른 무기로 전환
    IEnumerator WeaponToWeaponChange()
    {
        if(weaponTypeTemp!=WeaponType.Hand)
        {
            yield return StartCoroutine(UnEquipWeaponChanging());

        }
        yield return StartCoroutine(EquipWeaponChanging());
    }


    //무기 장착 해제
    IEnumerator UnEquipWeaponChanging()
    {

        
        weaponType = WeaponType.Hand;
        ChangingWeapon = true;
        movement.SetState(MecanimControl.PlayerState.P_ChangeWeapon);
        yield return new WaitForSeconds(weaponSwitchDelay);

            
        isEquipWeapon = false;
        ChangingWeapon = false;
        
        
        
    }

    //무기장착
    IEnumerator EquipWeaponChanging()
    {

        weaponType = weaponTypeTemp2;
        ChangingWeapon = true;

        movement.SetState(MecanimControl.PlayerState.P_ChangeWeapon);
        yield return new WaitForSeconds(weaponSwitchDelay);

        ChangingWeapon = false;
        isEquipWeapon = true;
        weaponTypeTemp = weaponTypeTemp2;
    }

    //무기장착 포지션,자식 지정
    public void EquipWeaponPos(Transform hand, Transform weapon)
    {
        if(weapon==bow)
        {
            weapon.position = bowEquipPos.position;
            weapon.rotation = bowEquipPos.rotation;
            weapon.parent = hand;
        }
        weapon.parent = hand;
    }
    public void UnEquipWeaponPos(Transform unEquipPos, Transform weapon)
    {
        weapon.position = unEquipPos.position;
        weapon.rotation = unEquipPos.rotation;
        weapon.parent = weaponSloot;       
        
    }
    public void UnEquipWeaponPos(Transform weapon)
    {
        weapon.parent = weaponSloot;
    }
    public void EquipWeaponEvent()
    {
        //무기 해제시 애니메이션 이벤트
        if (isEquipWeapon)
        {

            UnEquipWeaponPos(sword);
            UnEquipWeaponPos(bowUnEquipPos, bow);
           

        }
        else
        {   //무기 장착시 애니메이션 이벤트
            if (weaponType == WeaponType.Bow)
            {
                EquipWeaponPos(leftHand, bow);
            }
            else if(weaponType == WeaponType.Sword)
                EquipWeaponPos(rightHand, sword);
        }

    }

    public void AttackTrigger()
    {
        sword.GetComponent<BoxCollider>().isTrigger = true;
    }


    public void SetCallback(Callback_CreateMonster callback_setState)
    {
        if (setState != null)
        {
            Debug.Log("이미 설정된 callback_createMonster");
            return;
        }
        setState = callback_setState;
    }

}

//무기 타입
public enum WeaponType
{
    Hand,
    Sword,
    Bow,
}
