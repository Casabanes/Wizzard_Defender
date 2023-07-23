using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : IState
{
    private BaseEnemy _agent;
    public WaitState(BaseEnemy agent)
    {
        _agent = agent;
    }
    public void OnEnter()
    {
        _agent.animator.SetBool("idle", true);
    }

    public void OnExit()
    {
        _agent.animator.SetBool("idle", false);
    }

    public void OnUpdate()
    {
    }
    private void Wait()
    {
        if (_agent.nextState != EnemyStates.Wait)
        {
            _agent.GoToNextState();
        }
    }
}
