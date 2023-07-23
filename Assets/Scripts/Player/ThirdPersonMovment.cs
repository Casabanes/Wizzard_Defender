using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody),typeof(Transform))]
public class ThirdPersonMovment
{
    private Transform _transform;
    private Rigidbody _rigidBody;
    private Vector3 _direction;
    private float _speed = 5;
    public Action<float,float> Moving;
    private const int _constZero = 0;
    private bool _checker;
    public ThirdPersonMovment SetRigidBody(Rigidbody rb)
    {
        _rigidBody = rb;
        return this;
    }
    public ThirdPersonMovment SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    public void Move(float x, float z)
    {
        if(x == _constZero && z == _constZero)
        {
            if (!_checker)
            {
                DonrMove();
                _checker = true;
            }
            return;
        }
        _checker = false;
        Moving?.Invoke(x,z);
        _direction.y = 0;
        _direction = new Vector3(_transform.right.x * x + _transform.forward.x * z
            , 0, _transform.right.z * x + _transform.forward.z * z);

        _direction.Normalize();
        _direction *= _speed;
        _direction.y = _rigidBody.velocity.y;
        _rigidBody.velocity = _direction;
    }
    public void DonrMove()
    {
        Moving?.Invoke(_constZero, _constZero);
        _direction = Vector3.zero;
        _direction.y = _rigidBody.velocity.y;
        _rigidBody.velocity = _direction;
    }
    public ThirdPersonMovment SetTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }
}
