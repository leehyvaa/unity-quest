using UnityEngine;

public class PlayerIdle : PlayerState
{

    public PlayerIdle (MovementSM stateMachine) : base("Idle", stateMachine) {}

    public override void Enter()
    {
        base.Enter();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        //_horizontalInput = Input.GetAxis("Horizontal");
        //if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        //    stateMachine.ChangeState(sm.movingState);
    }

}
