using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    // Update is called once per frame
    void Update()
    {
        transform.forward = (lookAt.position - transform.position).normalized;
    }
}
