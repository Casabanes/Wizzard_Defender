using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractuable
{
    [SerializeField] private GameObject _shopUI;
    public void Interact()
    {
        _shopUI.SetActive(true);
    }
    public void GoOut()
    {
        _shopUI.SetActive(false);
    }
}
