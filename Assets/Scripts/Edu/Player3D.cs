using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static MecanimControl;

public class Player3D : MonoBehaviour
{
    public float moveSpeed = (float)MoveSpeedSetting.defaultMoveSpeed;
    public float rotationSpeed = 720.0f;


    Vector3 moveInput;

    CharacterController pcController;
    public Animator animator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        InitData();

    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterController_Slerp();
        NavMesh_Control();
    }

    private void NavMesh_Control()
    {
        if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //agent.destination = hit.point;
                agent.SetDestination(hit.point);
            }
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

 
    }
    private void InitData()
    {
       
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
       

    }
}
