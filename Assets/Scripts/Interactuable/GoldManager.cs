using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    private int _gold;
    public static GoldManager instance;
    public Text goldQuantity;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Add(int amount)
    {
        _gold += amount;
        goldQuantity.text = ""+_gold;
    }
    public void Substract(int amount)
    {
        _gold -= amount;
        goldQuantity.text =""+ _gold;

    }
}
