using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectPosition : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private Vector3 _offset;
    void Update()
    {
        if (_objectToFollow)
        {
            transform.position = _objectToFollow.position+ _offset;
        }
    }
}
