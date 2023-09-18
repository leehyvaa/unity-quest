using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tank : Car
{
    public Transform barrel;
    public Transform gun;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        BarrelMove();
    }

    void BarrelMove()
    {
        Vector3 lookFoward = new Vector3(cameraArm.forward.x,0f,cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        Vector3 moveDir = lookFoward * 1f + lookRight * 1f;

        barrel.Rotate(new Vector3(0f, cameraArm.rotation.y, 0f));
        //barrel.forward = cameraArm.forward;
        //gun.forward = cameraArm.forward;

    }
    
}
