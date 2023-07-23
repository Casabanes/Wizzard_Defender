using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLiveEntity : LiveEntity, IObservableToGenericBar
{
    [SerializeField] protected float _maxLife;
    [SerializeField] protected float _life;
    protected List<IObserverGenericBar> _observers = new List<IObserverGenericBar>();
    [SerializeField] private bool isPlayer;
    [SerializeField] private string _getHitTrigger;
    [SerializeField] private string _dieTrigger;
    [SerializeField] Animator _animator;
    protected override void Start()
    {
        _life = _maxLife;
    }
    protected override void Update()
    {
    }
    public override void Death()
    {
        NotifyIsEmptyToObserver();
        if(_animator&& _dieTrigger != null)
        {
            _animator.SetTrigger(_dieTrigger);
        }
        if (!isPlayer)
        {
            EnemyManager.instance.UnSuscribeEnemy();
        }
        else
        {
            GameManager.instance.Lose();
        }
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        _life -= damage;
        if(_animator && _getHitTrigger!=null)
        {
            _animator.SetTrigger(_getHitTrigger);
        }
        if (_life <= 0)
        {
            _life = 0;
            Death();
        }
        NotifyValueToObservers(_life);
    }
    public void Suscribe(IObserverGenericBar observer)
    {
        _observers.Add(observer);
        for (int count = 0; count < _observers.Count; count++)
        {
            _observers[count].SetMaxValue(_maxLife);
        }
    }

    public void NotifyValueToObservers(float value)
    {
        foreach (var item in _observers)
        {
            item.RefreshValue(value);
        }
    }

    public void NotifyIsEmptyToObserver()
    {
        foreach (var item in _observers)
        {
            item.NotifyBarIsEmpty(true);
        }
    }
}
