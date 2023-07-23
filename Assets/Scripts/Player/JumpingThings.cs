using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JumpingThings : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _jumpForce = 10;
    private ForceMode _jumpForceMode= ForceMode.Impulse;
    private int _numberOfGrounds;
    [SerializeField] private LayerMask _groundLayers;
    public Action<bool> Jumping;
    public Action Grounded;
    public JumpingThings SetRigidBody(Rigidbody rigidBody)
    {
        _rigidBody = rigidBody;
        return this;
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, _jumpForceMode);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            _numberOfGrounds++;
            Jumping?.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            _numberOfGrounds--;
            if (_numberOfGrounds < 1)
            {
                Jumping?.Invoke(IsGrounded());
            }
        }
    }
    public bool IsGrounded()
    {
        if (_numberOfGrounds > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
