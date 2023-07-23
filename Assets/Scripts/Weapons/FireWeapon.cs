using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FireWeapon : BaseWeapon
{
    [SerializeField] private FireBall fireball;
    [SerializeField] private Transform[] _additionalShotPositions;
    public Action shootingFireBall = delegate { };
    public Action shootFireBall;
    private bool _canAttack = true;
    [SerializeField] private float _fireBallBasicCost;
    [SerializeField] private float _fireBallBasicDamage;
    private float _numberOfFramesOfAnimationAttack = 14f;

    public override void StartAction1()
    {
        UseFireBall();
    }
    public override void SetPlayerModel(PlayerModel p,Transform t)
    {
        base.SetPlayerModel(p,t);
        shootFireBall += view.Attack01;
    }
    public void UseFireBall()
    {
        if (!_canAttack || player.mana.CurrentValue < _fireBallBasicCost * manaCostModifier)
        {
            return;
        }
        shootFireBall?.Invoke();
        player.animator.SetFloat(player.attackSpeedMultiplier, attacksPerSecond / 1);
        StartCoroutine(WaitToShoot());
        StartCoroutine(AttackCooldown());

    }
    private IEnumerator WaitToShoot()
    {
        float localAttacksPerSecond = _numberOfFramesOfAnimationAttack * (1 / attacksPerSecond);
        for (int i = 0; i < (int)localAttacksPerSecond; i++)
        {
            yield return null;
        }
        shootingFireBall?.Invoke();
        ShootFireBall(shootPosition.position);
    }
    public void ShootFireBall(Vector3 origin)
    {
            player.mana.SubstractAmount(_fireBallBasicCost * manaCostModifier);
            FireBall instance = Instantiate(fireball).SetPosition(origin)
                .SetDirection(player.ray.direction).SetDamage(_fireBallBasicDamage + damage);
        if (_additionalShotPositions.Length > 0)
        {
            foreach (var item in _additionalShotPositions)
            {
                if (player.mana.CurrentValue >= _fireBallBasicCost * manaCostModifier)
                {
                    player.mana.SubstractAmount(_fireBallBasicCost * manaCostModifier);
                    instance = Instantiate(fireball).SetPosition(origin)
                        .SetDirection(item.forward).SetDamage(_fireBallBasicDamage + damage);
                }
            }
        }
        _canAttack = false;
    }
    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1 / attacksPerSecond);
        _canAttack = true;
    }
}