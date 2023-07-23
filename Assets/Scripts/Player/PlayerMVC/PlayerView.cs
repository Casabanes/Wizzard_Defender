using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerView
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private string _x="x";
    [SerializeField] private string _y="y";
    [SerializeField] private string _z="z";
    [SerializeField] private string _isGrounded="isGrounded";
    [SerializeField] private string _attack01Trigger = "attack";
    [SerializeField] private string _attack02Trigger = "attack2";


    Action _UpdateMethods = delegate { };
    public PlayerView SetAnimator(Animator animator)
    {
        _animator = animator;
        return this;
    }
    public PlayerView SetRigidBody(Rigidbody rigidBody)
    {
        _rigidBody = rigidBody;
        return this;
    }
    public void OnUpdate()
    {
        _UpdateMethods();
    }
    public void Move(float x, float z)
    {
        _animator.SetFloat(_x, x); 
        _animator.SetFloat(_z, z);
    }
    private void VelocitY()
    {
        _direction.y = _rigidBody.velocity.y;
        Mathf.Clamp(_rigidBody.velocity.y,-1,1);
        _animator.SetFloat(_y, _direction.y);
    }
    public void Jump(bool isGrounded)
    {
        _animator.SetBool(_isGrounded, isGrounded);
        if (!isGrounded)
        {
            _UpdateMethods += VelocitY;
        }
        else
        {
            _UpdateMethods -= VelocitY;
        }
    }
    public void Attack01()
    {
        _animator.SetTrigger(_attack01Trigger);
    }
    public void Attack02(bool value)
    {
            _animator.SetBool(_attack02Trigger,value);
    }
}
