using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Movement,
    Attack,
    Stuned,
    Wait
}
public class FSM
{
    private Dictionary<EnemyStates, IState> _allStates=new Dictionary<EnemyStates, IState>();
    private IState _currentState;

    public void OnUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }
    public IState GetCurrentState()
    {
        return _currentState;
    }
    public void AddState(EnemyStates key, IState state)
    {
        if (_allStates.ContainsKey(key))
        {
            return;
        }
            _allStates.Add(key, state);
    }
    public void ChangeState(EnemyStates key)
    {
        if (_allStates.ContainsKey(key))
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
            }
            _currentState = _allStates[key];
            _currentState.OnEnter();
        }
    }
}