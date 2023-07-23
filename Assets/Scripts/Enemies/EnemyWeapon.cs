using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private bool _itHit = false;
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _speed;
    private Vector3 _direction;

    void Start()
    {
        _direction = (GameManager.instance.GetPlayer().transform.position + Vector3.up *0.5f - transform.position).normalized;
    }
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_itHit)
        {
            BasicLiveEntity target = collision.gameObject.GetComponent<BasicLiveEntity>();
            if (target)
            {
                target.TakeDamage(_damage);
            }
        }
        _itHit = true;
        Destroy(gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!_itHit)
        {
            BasicLiveEntity target = collision.gameObject.GetComponent<BasicLiveEntity>();
            if (target)
            {
                target.TakeDamage(_damage);
            }
        }
        _itHit = true;
        Destroy(gameObject);
    }
}
