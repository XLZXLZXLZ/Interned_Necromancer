using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Skill", fileName = "Player_Skill")]
public class Player_SkillState : PlayerStates
{
    public override void Enter()
    {
        base.Enter();

        anim.Play("Spell");
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
        if (ControllerManager.Instance.leaveSoul)
        {
            stateMachine.SwitchState(typeof(Player_LeaveState));
            return;
        }
        if (!controller.IsGround) 
        {
            stateMachine.SwitchState(typeof(Player_FallState));
            return;
        }

        if (!controller.IsMoving) 
        {
            stateMachine.SwitchState(typeof(Player_IdleState));
            return;
        }

        if (controller.IsMoving) 
        {
            stateMachine.SwitchState(typeof(Player_RunState));
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!(controller.IsWall && input.axes.x * player.transform.localScale.x > 0))
            controller.SetVelocityX(input.axes.x * info.speed);
        controller.FaceAdjust(Tools.MousePosition.x - player.transform.position.x);
        //controller.SetVelocityX(0);
    }

}
