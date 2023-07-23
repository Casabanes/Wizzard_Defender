using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public FSM stateMachine;
    [SerializeField] protected float _damage;
    protected float attackTiming;
    public float attacksPerSecond;
    protected float _rangeToAttack;
    public float _speed;
    public Rigidbody rigidBody;
    public Movement movement;
    public Animator animator;
    private EnemyStates _firstState;
    public List<Transform> waypoints;
    [SerializeField] private float _time;
    [SerializeField] private float _timeToRotate;
    private Vector3 _startingForward;
    [SerializeField] private int[] _layersToAttack;
    public bool isInRange;
    public bool isWaiting;
    public float stunedTime;
    public EnemyStates nextState;
    public BasicLiveEntity target;
    private void Start()
    {
        GameManager.instance.LoseGame += WhenPlayerLose;
        movement = new Movement().SetTransform(transform).SetRigidBody(rigidBody).SetSpeed(_speed);
        stateMachine = new FSM();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        stateMachine.AddState(EnemyStates.Movement, new MovementState(this));
        stateMachine.AddState(EnemyStates.Attack, new AttackState(this));
        stateMachine.AddState(EnemyStates.Stuned, new StunedState(this));
        stateMachine.AddState(EnemyStates.Wait, new WaitState(this));

        stateMachine.ChangeState(nextState);
        EnemyManager.instance.SuscribeEnemy();
    }
    private void Update()
    {
        stateMachine.OnUpdate();
    }
    public virtual void Attack()
    {
        StartCoroutine(WaitUntilFinishAttack());
        DoSomeThingAtAttackMoment();
    }
    protected virtual  IEnumerator WaitUntilFinishAttack()
    {
        yield return new WaitForSeconds(0.25f);
        SetNextState();
        if (nextState != EnemyStates.Attack)
        {
            GoToNextState();
        }
    }
    protected virtual void DoSomeThingAtAttackMoment(){}
    public void RotateToDirection(Vector3 direction)
    {
        if (_startingForward == Vector3.zero)
        {
            _startingForward = transform.forward;
        }
        _time += Time.deltaTime;
        if (_time >= _timeToRotate)
        {
            _startingForward = Vector3.zero;
            _time = 0;
            return;
        }
        transform.forward = Vector3.Slerp(_startingForward, direction, _time / _timeToRotate);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.position.z < transform.position.z)
        {
        WaitOrNotDependingCollision(collision, true);
        StartCoroutine(MoveAfterWait());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        WaitOrNotDependingCollision(collision, false);
    }
    private void WaitOrNotDependingCollision(Collision collision ,bool yesOrNot)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            isWaiting = yesOrNot;
            SetNextState();
        }
    }
    private IEnumerator MoveAfterWait()
    {
        yield return new WaitForSeconds(0.5f);
        nextState = EnemyStates.Movement;
        GoToNextState();
    }
    private void OnTriggerEnter(Collider other)
    {
        SetNextStateDependingTrigger(other, true);
    }
    private void OnTriggerExit(Collider other)
    {
        SetNextStateDependingTrigger(other, false);
    }
    private void SetNextStateDependingTrigger(Collider other, bool isinrange)
    {
        for (int i = 0; i < _layersToAttack.Length; i++)
        {
            if (other.gameObject.layer == _layersToAttack[i])
            {
                isInRange = isinrange;
                if (isinrange)
                {
                    target = other.gameObject.GetComponent<BasicLiveEntity>();
                }
                SetNextState();
                GoToNextState();
            }
        }
    }
    public void Stun(float time)
    {
        stunedTime = time;
        stateMachine.ChangeState(EnemyStates.Stuned);
    }
    public void SetNextState()
    {
        if (isInRange)
        {
            nextState = EnemyStates.Attack;
            return;
        }
        if (isWaiting)
        {
            nextState = EnemyStates.Wait;
        }
        else
        {
            nextState = EnemyStates.Movement;
        }
    }
    public void GoToNextState()
    {
        stateMachine.ChangeState(nextState);
    }
    public void WhenPlayerLose()
    {
        Destroy(gameObject);
    }
}
