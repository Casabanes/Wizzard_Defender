using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : BaseEnemy
{
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private GameObject _handWeapon;
    [SerializeField] private Transform _shootPoint;
    protected override void DoSomeThingAtAttackMoment()
    {
        StartCoroutine(LaunchWeapon());
    }
    private IEnumerator LaunchWeapon()
    {
        yield return new WaitForSeconds(0.65f);
        Instantiate(_weapon, _shootPoint.position, _shootPoint.rotation);
        _handWeapon.SetActive(false);
        StartCoroutine(TurnOnHandWeapon());
    }
    private IEnumerator TurnOnHandWeapon()
    {
        yield return new WaitForSeconds(0.25f);
        _handWeapon.SetActive(true);
    }
}
