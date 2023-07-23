using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : GenericBar
{
    [SerializeField] private ManaComponent _entity;
    protected override void Start()
    {
        base.Start();
        _entity.Suscribe(this);

    }
}
