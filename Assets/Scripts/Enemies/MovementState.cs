using UnityEngine;

public class MovementState : IState
{
    private BaseEnemy _agent;
    private int _index=0;
    private Vector3 _direction;
    private float _minDistanceToSelectNextWaypoint=0.25f;
    private float _currentDistance;
    private Vector3 _targetPosition;

    public MovementState(BaseEnemy agent)
    {
        _agent = agent;
        _targetPosition = _agent.waypoints[0].position;
    }

    public void OnEnter()
    {
        if (_index >= _agent.waypoints.Count)
        {
            if (_agent.nextState != EnemyStates.Movement)
            {
                _agent.GoToNextState();
                _agent.movement.Move(0,0,0);
            }
            else
            {
                _agent.nextState = EnemyStates.Wait;
                _agent.GoToNextState();
                _agent.movement.Move(0,0,0);
            }
        }
        _agent.animator.SetBool("moving", true);
    }

    public void OnExit()
    {
        _agent.animator.SetBool("moving", false);
    }

    public void OnUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _direction = (_targetPosition - _agent.transform.position).normalized * _agent._speed*Time.deltaTime;
        _currentDistance = Vector3.Distance(_agent.transform.position, _agent.waypoints[_index].position);
        if (_currentDistance <= _minDistanceToSelectNextWaypoint)
        {
            SetDirection();
        }
        _agent.movement.Move(_direction.x,_direction.y,_direction.z);
        _agent.RotateToDirection(_direction.normalized);
        _agent.SetNextState();
        if (_agent.nextState != EnemyStates.Movement)
        {
            _agent.GoToNextState();
        }
    }
    private void SetDirection()
    {
        _index++;
        if(_index<_agent.waypoints.Count)
        {
            _targetPosition = _agent.waypoints[_index].position;
        }
        else
        {
            if (_agent.nextState != EnemyStates.Movement)
            {
                _agent.GoToNextState();
                _agent.movement.Move(0,0,0);
            }
            else
            {
                _agent.nextState = EnemyStates.Wait;
                _agent.GoToNextState();
                _agent.movement.Move(0,0,0);
            }
        }
    }
}
