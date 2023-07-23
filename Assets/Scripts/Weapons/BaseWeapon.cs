using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float damage;
    public float attacksPerSecond;
    public float manaCostModifier;
    public Transform shootPosition;
    public PlayerModel player;
    protected PlayerView view;
    public virtual void SetPlayerModel(PlayerModel p,Transform t)
    {
        player = p;
        view = player.GetComponent<PlayerCharacter>().GetPlayerView();
        transform.position = t.position;
        transform.rotation = t.rotation;
    }
    public virtual void StartAction1()
    {

    }
    public virtual void MantainAction1()
    {

    }
    public virtual void ExitAction1()
    {

    }
    public virtual void StartAction2()
    {

    }
    public virtual void MantainAction2()
    {

    }
    public virtual void ExitAction2()
    {

    }
}
