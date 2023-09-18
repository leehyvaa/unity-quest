using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ProjectileBase : MonoBehaviour
{
    public GameObject owner { get; private set; }
    public Vector3 initialPosition { get; private set; }
    public Vector3 initialDirection { get; private set; }

    public UnityAction Onshoot;

    public void Shoot(BowController controller)
    {
        owner = controller.Owner;
        initialPosition = transform.position;
        initialDirection = transform.forward;

        if (Onshoot != null)
            Onshoot.Invoke();
    }

 }
