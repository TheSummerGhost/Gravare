using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_PlayerDetectedState : PlayerDetectedState
{
    private Archer archer;
    public Archer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Archer archer) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.archer = archer;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            if (Time.time >= archer.dodgeState.startTime + archer.dodgeStateData.dodgeCooldown) // when did we last dodge check
            {
                stateMachine.ChangeState(archer.dodgeState);

            } else
            {
                stateMachine.ChangeState(archer.meleeAttackState);
            }

        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(archer.lookForPlayerState);
            //Debug.Log("is not in max agro range");
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
