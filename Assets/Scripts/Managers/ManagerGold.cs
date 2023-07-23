using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGold : MonoBehaviour
{
    private int _gold;

    public void AddGold(int amount)
    {
        _gold += amount;
    }
    public void Substract(int amount)
    {
        _gold -= amount;
    }
    public int GetGoldAmount()
    {
        return _gold;
    }
}
