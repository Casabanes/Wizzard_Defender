using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunedState : IState
{
    private BaseEnemy _agent;
    private float _time;
    public StunedState(BaseEnemy agent)
    {
        _agent = agent;
    }
    public void OnEnter()
    {
        _time = 0;
        _agent.animator.SetBool("idle", true);
    }

    public void OnExit()
    {
        _agent.animator.SetBool("idle", false);
    }

    public void OnUpdate()
    {
        _time += Time.deltaTime;
        if (_time >= _agent.stunedTime)
        {
            _agent.SetNextState();
            _agent.GoToNextState();
        }
    }
}
