using UnityEngine;

public class PlayerMoving : PlayerState
{

    Vector3 moveInput;

    public PlayerMoving(MovementSM stateMachine) : base("Moving", stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        sm.spriteRenderer.color = Color.red;
        moveInput = Vector3.zero;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (moveInput.magnitude == 0)
            stateMachine.ChangeState(sm.idleState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 vel = sm.rigidbody.velocity;
        vel.x = _horizontalInput * ((MovementSM)stateMachine).speed;
        sm.rigidbody.velocity = vel;


        Vector3 dir = foward.transform.position - transform.position;
        dir.Normalize();

        Vector3 move = dir * engine;
        Vector3 newPos = sm.rigidbody.position + move * moveSpeed * Time.deltaTime;
        sm.rigidbody.MovePosition(newPos);
    }

}
