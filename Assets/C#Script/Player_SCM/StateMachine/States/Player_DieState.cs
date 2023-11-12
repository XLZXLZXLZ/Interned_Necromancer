using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Die", fileName = "Player_Die")]
public class Player_DieState : PlayerStates
{
    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        switch(death.deathReason)
        {
            case DeathType.Burn:
                anim.Play("Die1");
                break;
            case DeathType.Fall:
                anim.Play("Die2");
                break;
            default:
                anim.Play("Die0");
                break;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (death.deathReason != DeathType.Null)
            return;

        stateMachine.SwitchState(typeof(Player_IdleState));
    }

    public override void Exit()
    {
        rb.gravityScale = 1.8f;
        base.Exit();
    }
}
