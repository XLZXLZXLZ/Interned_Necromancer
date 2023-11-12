using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Leave", fileName = "Player_Leave")]
public class Player_LeaveState : PlayerStates
{
    public override void Enter()
    {
        base.Enter();

        anim.Play("Leave");
    }

    public override void LogicUpdate()
    {
        if (!AnimEnd)
            return;

        if (death.deathReason != DeathType.Null)
        {
            stateMachine.SwitchState(typeof(Player_DieState));
            return;
        }
        if (!ControllerManager.Instance.leaveSoul)
            stateMachine.SwitchState(typeof(Player_IdleState));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        controller.SetVelocityX(0);
    }
}
