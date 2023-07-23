using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : BaseEnemy
{
    [SerializeField] private MeleEnemyWeapon _weapon;
    protected override void DoSomeThingAtAttackMoment()
    {
        _weapon.Turn(true);
        StartCoroutine(TurnOffWeapon());
    }
    private IEnumerator TurnOffWeapon()
    {
        yield return new WaitForSeconds(0.8f);
        _weapon.Turn(false);
    }
}
