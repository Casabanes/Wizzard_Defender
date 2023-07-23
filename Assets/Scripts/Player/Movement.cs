using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement
{
    private Rigidbody _rigidBody;
    private Vector3 _direction;
    private float _speed=5;
    private Transform _transform;

    public Movement SetTransform(Transform transform)
    {
        _transform = transform;
        return this;
    }
    public Movement SetRigidBody(Rigidbody rb)
    {
        _rigidBody = rb;
        return this;
    }
    public Movement SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
    public void Move(float x,float y, float z)
    {
        _direction.y = y;
        _direction.x = x;
        _direction.z = z;
        _direction.Normalize();
        _direction *= _speed;
        _rigidBody.velocity = _direction;
        if (_direction.normalized != Vector3.zero)
        {
            _transform.forward = _direction.normalized;
        }
    }
}
