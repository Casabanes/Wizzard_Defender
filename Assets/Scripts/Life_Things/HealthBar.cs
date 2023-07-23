using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : GenericBar
{
    [SerializeField] private BasicLiveEntity _entity;
    protected override void Start()
    {
        base.Start();
        SetTarget(_entity.GetComponent<IObservableToGenericBar>());
    }
}
