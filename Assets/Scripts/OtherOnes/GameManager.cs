using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _player;
    //public event winEvent;
    //public event loseEvent;
    public GameObject WinUI;
    public GameObject LoseUI;
    public event Action WinGame;
    public event Action LoseGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Component CheckComponent(Component c, GameObject o,Type type)
    {
        if (!c)
        {
            c = o.GetComponent(type);
            if (!c)
            {
                Debug.LogError("The " + type + " component does not exist in the "+ o.name +" object");
                o.gameObject.SetActive(false);
                return c;
            }
        }
        return c;
    }
    public GameObject GetPlayer()
    {
        if (!_player)
        {
            Debug.Log("No hay player, que paso?");
            return gameObject;
        }
        return _player;
    }
    public void Win()
    {
        WinUI.SetActive(true);
        WinGame?.Invoke();
    }
    public void Lose()
    {
        LoseUI.SetActive(true);
        LoseGame?.Invoke();
    }
}
