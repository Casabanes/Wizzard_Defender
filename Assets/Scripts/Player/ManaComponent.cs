using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaComponent : MonoBehaviour,IObservableToGenericBar
{
    [SerializeField] private float _currentValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _regenerationPerSecond;
    private const int _constZero = 0;
    protected List<IObserverGenericBar> _observers = new List<IObserverGenericBar>();
    private void Start()
    {
        _currentValue = _maxValue;

    }
    public float CurrentValue
    {
        get
        {
            return _currentValue;
        }
    }
    private void Update()
    {
        RegenerationOverTime();
    }
    public void RegenerationOverTime()
    {
        AddAmount(_regenerationPerSecond * Time.deltaTime);
        NotifyValueToObservers(_currentValue);
    }
    public void SubstractAmount(float amount)
    {
        _currentValue -= amount;
        if(_currentValue < _constZero)
        {
            _currentValue = _constZero;
        }
        NotifyValueToObservers(_currentValue);
    }
    public void AddAmount(float amount)
    {
        _currentValue += amount;
        if (_currentValue > _maxValue)
        {
            _currentValue = _maxValue;
        }
        NotifyValueToObservers(_currentValue);
    }
    public void Suscribe(IObserverGenericBar observer)
    {
        _observers.Add(observer);
        for (int count = 0; count < _observers.Count; count++)
        {
            _observers[count].SetMaxValue(_maxValue);
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
