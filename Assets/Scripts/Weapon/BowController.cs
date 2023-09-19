using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{


    public MecanimControl movement;

    public bool aming = false;

    public float amingDelay = 0.5f;
    public float amingDealyCount = 0f;

    public WeaponManager weaponManager;
    public Transform arrowSpawner;
    public Transform releaseArrows;
    
    public GameObject arrowPrefab;
    public GameObject arrowPrefab2;
    public GameObject arrow;
    public Transform aimPoint;
    public Transform aimPoint2;
    public GameObject Owner { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Owner = gameObject;
        movement = GetComponentInParent<MecanimControl>();
        weaponManager = GetComponent<WeaponManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 좌 클릭시 aming 전환
        if (Input.GetKey(KeyCode.Mouse0))
        {
            amingDealyCount += Time.deltaTime;
            if(amingDealyCount > amingDelay)
            {
                aming = true;
                amingDealyCount = 0;
            }
        }    
        else
        {
            aming = false;

        }

        if (aming)
            amingDealyCount = 0;

        //활쏘기 모션 전환
        if ((int)WeaponManager.weaponType == 2 && aming)
        {
            
            //return;
            if (movement.aimCount>movement.aimCooltime)
            {
                movement.SetState(MecanimControl.PlayerState.P_Aiming);

            }
        }


        movement.aimCooltime = 1;
        movement.aimCount += Time.deltaTime;

    }
    private void FixedUpdate()
    {
        movement.animator.SetBool("Aming", aming);
    }



    public void BowShoot()
    {
        Debug.Log("Shoot");
        movement.moveSpeed = (float)MecanimControl.MoveSpeedSetting.fightMoveSpeed;
        arrow.transform.LookAt(aimPoint);
        Transform arrowPosition = arrow.transform;
        Destroy(arrow);
        arrow = null;

        Vector3 shotDiraction = aimPoint.position - arrowPosition.position;
        GameObject releaseArrow = Instantiate(arrowPrefab2,arrowPosition.position,Quaternion.LookRotation(shotDiraction));
        ProjectileBase newProjectile = releaseArrow.GetComponent<ProjectileBase>();
        if (newProjectile != null)
            newProjectile.Shoot(this);
        
        releaseArrow.transform.parent = releaseArrows;
        

    }
    public void RecoilRelease()
    {
        
        movement.SetState(MecanimControl.PlayerState.P_Idle);
        
        
        movement.aimCount = 0;
        Debug.Log("Release");

    }
    
    public void ArrowSpawn()
    {
        Debug.Log("1");
        arrow = Instantiate(arrowPrefab, arrowSpawner);
        arrow.transform.parent = weaponManager.rightHand;
        movement.moveSpeed = 0f;
    }

    public void AimDrow()
    {
        movement.moveSpeed = (float)MecanimControl.MoveSpeedSetting.fightMoveSpeed;
    }

}
