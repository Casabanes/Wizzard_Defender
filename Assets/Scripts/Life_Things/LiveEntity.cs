using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiveEntity : MonoBehaviour
{
    protected const int _constZero = 0;
    protected virtual void Start(){}
    protected virtual void Update(){}
    public abstract void TakeDamage(float damage);
    public abstract void Death();
}
