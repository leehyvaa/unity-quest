using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public delegate void Callback_CreateMonster(int id);
    Callback_CreateMonster setState;

    //���� 3�� �Ǽ� �� ���� / Ű1,2,3���� ����
    //�޼� ������ ������ ����
    //�ִϸ��̼ǿ��� �̺�Ʈ�� ����,�����ϰԲ� �Լ� �ۼ�
    //


    //Ȱ ���� Ȥ�� ������ ������, ��������
    public Transform bowEquipPos;
    public Transform bowUnEquipPos;
    public Transform weaponSloot;

    //���� ���� ��ġ (�޼հ� ������)
    public Transform leftHand;
    public Transform rightHand;

    //���� Ʈ������
    public Transform sword;
    public Transform bow;

    //���� �������� 
    public bool isEquipWeapon = false;

    //public static Transform currentWeapon; //���� ����
    //public static Animator currentWeaponAnim; //���� ������ �ִϸ��̼�

    //���� ��ü �ߺ� ���� ����
    public static bool ChangingWeapon = false;
    //���� �ٲܶ� ������
    public float weaponSwitchDelay = 1.15f;

    public static WeaponType weaponType; //���� ����

    public WeaponType weaponTypeTemp; // ���⿡�� �Ǽ����� ��ȯ�Ҷ� ����ϴ� ���� ������� �Ǽտ��� 1-x Ȥ�� 2-x �Է�
    public WeaponType weaponTypeTemp2; // ���⿡�� �ٸ������ ��ȯ�Ҷ� ����ϴ� ���� 1-2 Ȥ�� 2-1 �Է� 
    
    //public MecanimControl movement;


    public GameObject Owner { get; set; }

    //��ǲ
    //private PlayerInputHandler playerInput;

    //private SwordController theSwordController; ���� ������� SwordController.cs Ȱ��ȭ
    //private BowController theSwordController; Ȱ�� ������� BowController.cs Ȱ��ȭ
    private void Start()
    {
        movement = GetComponentInParent<MecanimControl>();
        weaponType = WeaponType.Hand;
        isEquipWeapon = false;
        bow.position = bowUnEquipPos.position;
    }

    private void Update()
    {
        //�Ǽ�
        if (Input.GetKeyDown(KeyCode.X) && (weaponType !=WeaponType.Hand))
        {
            if (movement.pState != MecanimControl.PlayerState.P_Idle)
                return;

            movement.tempMoveSpeed = movement.moveSpeed;
            weaponTypeTemp = weaponType;
            StartCoroutine(UnEquipWeaponChanging());
        }

        //Į�̱�
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

        //Ȱ�̱�
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
        //�ִϸ��̼� ���� ������Ʈ
        movement.ani.SetInteger("wState", (int)weaponType);
        movement.ani.SetInteger("wTemp", (int)weaponTypeTemp);

        //movement.ani.SetBool("wState",ChangingWeapon);
        movement.ani.SetBool("isEquipWeapon", isEquipWeapon);
        movement.ani.SetFloat("wTypeBlend", (float)(weaponType-1));
    }

    //���⿡�� �ٸ� ����� ��ȯ
    IEnumerator WeaponToWeaponChange()
    {
        if(weaponTypeTemp!=WeaponType.Hand)
        {
            yield return StartCoroutine(UnEquipWeaponChanging());

        }
        yield return StartCoroutine(EquipWeaponChanging());
    }


    //���� ���� ����
    IEnumerator UnEquipWeaponChanging()
    {

        
        weaponType = WeaponType.Hand;
        ChangingWeapon = true;
        movement.SetState(MecanimControl.PlayerState.P_ChangeWeapon);
        yield return new WaitForSeconds(weaponSwitchDelay);

            
        isEquipWeapon = false;
        ChangingWeapon = false;
        
        
        
    }

    //��������
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

    //�������� ������,�ڽ� ����
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
        //���� ������ �ִϸ��̼� �̺�Ʈ
        if (isEquipWeapon)
        {

            UnEquipWeaponPos(sword);
            UnEquipWeaponPos(bowUnEquipPos, bow);
           

        }
        else
        {   //���� ������ �ִϸ��̼� �̺�Ʈ
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
            Debug.Log("�̹� ������ callback_createMonster");
            return;
        }
        setState = callback_setState;
    }

}

//���� Ÿ��
public enum WeaponType
{
    Hand,
    Sword,
    Bow,
}
