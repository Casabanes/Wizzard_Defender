using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : BaseEnemy
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadious;
    protected override void DoSomeThingAtAttackMoment()
    {
        StartCoroutine(Explode());
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.75f);
        Instantiate(_effect, _shootPoint.transform.position, _shootPoint.transform.rotation);
        target.TakeDamage(_damage);
        target.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _shootPoint.transform.position
            , _explosionRadious);
        EnemyManager.instance.UnSuscribeEnemy();
        Destroy(gameObject);
    }
}
