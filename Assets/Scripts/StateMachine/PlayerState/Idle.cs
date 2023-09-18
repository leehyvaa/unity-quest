using UnityEngine;

public class PlayerIdle : PlayerState
{
    private float _horizontalInput;

    public PlayerIdle (MovementSM stateMachine) : base("Idle", stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        sm.spriteRenderer.color = Color.black;
        _horizontalInput = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
            stateMachine.ChangeState(sm.movingState);
    }

}
