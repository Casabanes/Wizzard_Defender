using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    private ThirdPersonMovment _movement;
    [SerializeField] private JumpingThings _jumpThings;
    public BaseWeapon _weapon;
    public string attackSpeedMultiplier = "attackSpeedMultiplier";
    public Ray ray;
    public ManaComponent mana;
    public Transform[] showPoint;
    [SerializeField] private Transform weaponPos;
    public LayerMask spellBounds;
    public Animator animator;
    void Start()
    {
        if (mana == null)
        {
            mana = GetComponent<ManaComponent>();
            if (mana == null)
            {
                Debug.Log("Fatal error, mana component not found");
                gameObject.SetActive(false);
            }
        }
        _rigidBody = (Rigidbody)GameManager.instance.CheckComponent(_rigidBody, gameObject, typeof(Rigidbody));

        if (_movement == null)
        {
            _movement = new ThirdPersonMovment().SetTransform(transform)
                                                .SetRigidBody(_rigidBody);
        }
        if (_jumpThings == null)
        {
            _jumpThings = (JumpingThings)GameManager.instance.CheckComponent(_jumpThings, gameObject, typeof(JumpingThings));
        }
        _jumpThings.SetRigidBody(_rigidBody);
        if (_weapon == null)
        {
            Debug.Log("No tiene un arma asginada");
            gameObject.SetActive(false);
        }
        _weapon.SetPlayerModel(this, weaponPos);
    }
    private void Update()
    {
        Aiming();
    }
    public ThirdPersonMovment GetMovement()
    {
        if (_movement == null)
        {
            _movement = new ThirdPersonMovment().SetTransform(transform)
                                            .SetRigidBody(_rigidBody);
        }
        return _movement;
    }
    public JumpingThings GetJumpingThings()
    {
        return _jumpThings;
    }
    public void Aiming()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, spellBounds))
        {
            foreach (var item in showPoint)
            {
                item.position = raycastHit.point;
            }
        }
    }
    #region AcctionsOfWeapons
    public virtual void StartAction1()
    {
        _weapon.StartAction1();
    }
    public virtual void MantainAction1()
    {
        _weapon.MantainAction1();
    }
    public virtual void ExitAction1()
    {
        _weapon.ExitAction1();
    }
    public virtual void StartAction2()
    {
        _weapon.StartAction2();
    }
    public virtual void MantainAction2()
    {
        _weapon.MantainAction2();
    }
    public virtual void ExitAction2()
    {
        _weapon.ExitAction2();
    }
    #endregion
}
