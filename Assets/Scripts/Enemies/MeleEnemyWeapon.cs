using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemyWeapon : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private BoxCollider _boxCollider;
    public void Turn(bool value)
    {
        _boxCollider.enabled = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        BasicLiveEntity target = other.gameObject.GetComponent<BasicLiveEntity>();
        if (target)
        {
            target.TakeDamage(_damage);
            Turn(false);
        }
    }   
}
