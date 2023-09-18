using UnityEngine;

public class PlayerState : BaseState
{
    protected MovementSM sm;


    public PlayerState(string name, MovementSM stateMachine) : base(name, stateMachine)
    {
        sm = (MovementSM) this.stateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(sm.jumpingState);
    }

}
