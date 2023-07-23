using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private BaseEnemy _agent;
    private float _time;

    public AttackState(BaseEnemy agent)
    {
        _agent = agent;
    }
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        Attack();
    }
    private void Attack()
    {
        if (_agent.target != null)
        {
            _agent.RotateToDirection(_agent.target.transform.position - _agent.transform.position);
        }
        _time += Time.deltaTime;
        if (_time >= _agent.attacksPerSecond)
        {
            _agent.Attack();
            _agent.animator.SetTrigger("attack");
            _time = 0;
        }
    }
}
