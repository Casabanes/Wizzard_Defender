using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractuable
{
    [SerializeField] private int _amount;
    public void Interact()
    {
        if (_amount > 0)
        {
            GoldManager.instance.Add(_amount);
            Destroy(gameObject);
        }
    }
}
