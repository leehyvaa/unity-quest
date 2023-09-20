using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActiveState : PlayerState
{
    public PlayerActiveState(MovementSM stateMachine) : base("PlayerActive", stateMachine) { }

    MoveState moveState = MoveState.Idle;

    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdatePhysics()
    {
        switch (moveState)
        {
            case MoveState.Idle:
                {

                }
                break;
            case MoveState.Move:
                {
                    Move();
                }
                break;
            case MoveState.Jump:
                {
                    Jump();
                }
                break;
            case MoveState.Roll:
                {
                    
                }
                break;
            case MoveState.Attack:
                {

                }
                break;
        }
    }

    public override void UpdateLogic()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        switch (moveState)
        {
            case MoveState.Idle:
                {

                }break;
            case MoveState.Move:
                {

                }
                break;
            case MoveState.Jump:
                {

                }
                break;
            case MoveState.Roll:
                {

                }
                break;
            case MoveState.Attack:
                {

                }
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected virtual void Move()
    {

    }
    protected virtual IEnumerator Jump()
    {
        yield return null;
    }

    protected virtual IEnumerator Roll()
    {
        yield return null;
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
    }

    protected void SetMoveState(MoveState state)
    {
        moveState = state;
        SetAniParam("pState", (int)state);
    }

    protected enum MoveState
    {
        Idle,
        Move,
        Jump,
        Roll,
        Attack,
    }

}
