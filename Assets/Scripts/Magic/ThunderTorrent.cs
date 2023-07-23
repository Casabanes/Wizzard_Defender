using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTorrent : MonoBehaviour
{
    private float _damage;
    private float _attackSpeed;
    private bool _canAttack = true;
    private Vector3 _newposition;
    [SerializeField] private int[] _damageLayers;
    public void Use(Vector3 position, float attackSpeed, float damage)
    {
        _newposition = position;
        _attackSpeed = attackSpeed;
        _damage= damage;
    }
    private void Update()
    {
        transform.position = _newposition;
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in _damageLayers)
        {
            if (other.gameObject.layer == item && _canAttack)
            {
                other.GetComponent<LiveEntity>().TakeDamage(_damage);
                StartCoroutine(AttackCooldown());
            }
        } 
    }
    private void OnTriggerStay(Collider other)
    {
        foreach (var item in _damageLayers)
        {
            if (other.gameObject.layer == item && _canAttack)
            {
                other.GetComponent<LiveEntity>().TakeDamage(_damage);
                StartCoroutine(AttackCooldown());
            }
        }
    }
    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _canAttack = true;
    }
}
