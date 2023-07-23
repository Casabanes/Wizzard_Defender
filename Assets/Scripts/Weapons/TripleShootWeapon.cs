using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShootWeapon : BaseWeapon
{
    [SerializeField] private Transform[] _extraPoints;
    [SerializeField] private PlayerModel _player;

    private void Start()
    {
       // _player.shootingFireBall += ExtraFireballShoot;
    }
    public void ExtraFireballShoot()
    {
        foreach (var item in _extraPoints)
        {
          //  _player.ShootFireBall(item.position);
        }
    }
}
