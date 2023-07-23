using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDroped : MonoBehaviour, IInteractuable
{
    private BaseWeapon _weapon;
    public void Interact()
    {
        PlayerModel player = FindObjectOfType<PlayerModel>();
        BaseWeapon aux = player._weapon;
        player._weapon = _weapon;
        _weapon = aux;
    }
}
