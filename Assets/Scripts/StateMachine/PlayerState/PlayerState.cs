using UnityEngine;

public class PlayerState : BaseState
{
    protected MovementSM sm;
    protected Vector3 moveInput;
    

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

    protected void SetAniParam(string paramName, int value)
    {
        sm.ani.SetInteger(paramName, value);
    }

    protected void SetAniParam(string paramName, float value)
    {
        sm.ani.SetFloat(paramName, value);
    }

    protected void SetAniParam(string paramName, bool value)
    {
        sm.ani.SetBool(paramName, value);
    }
}
