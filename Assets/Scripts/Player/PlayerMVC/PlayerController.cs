using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController
{

    PlayerModel _playerModel;
    ThirdPersonMovment _playerMovement;
    JumpingThings _playerJumping;

    private string _axisVerticalKey = "Vertical";
    private string _axisHorizontalKey = "Horizontal";
    private KeyCode _primarySpellKey = KeyCode.Mouse0;
    private KeyCode _secondarySpellKey = KeyCode.Mouse1;
    private KeyCode _jumpKey = KeyCode.Space;
    /* //ideas de principio de desarrollo
    private KeyCode _choseSpellOneKey = KeyCode.Alpha1;
    private KeyCode _choseSpellTwoKey = KeyCode.Alpha2;
    private KeyCode _choseSpellThreeKey = KeyCode.Alpha3;
    private KeyCode _choseSpellFourKey = KeyCode.Alpha4;
    private KeyCode _choseSpellFiveKey = KeyCode.Alpha5;
    private KeyCode _choseSpellSixKey = KeyCode.Alpha6;
    private KeyCode _choseSpellSevenKey = KeyCode.Alpha7;
    private KeyCode _choseSpellEightKey = KeyCode.Alpha8;
    private KeyCode _choseSpellNineKey = KeyCode.Alpha9;
    private KeyCode _choseSpellTenKey = KeyCode.Alpha0;
    private KeyCode _choseSecretSpellKey = KeyCode.KeypadEnter;
    private KeyCode _defendKey = KeyCode.E;
    private KeyCode _chargeKey = KeyCode.Q;
    private KeyCode _potionDrinkKey = KeyCode.R;
    private KeyCode _dashKey = KeyCode.LeftShift;
    private KeyCode _secondaryDashKey = KeyCode.RightShift;
    private KeyCode _dieKey = KeyCode.O; //provisional
    private KeyCode _dieRecoveryKey = KeyCode.U; //provisional
    */ //ideas de principio de desarrollo

    private KeyCode _interactPickUpKey = KeyCode.F;



    public Action normalControls;
    public Action actualControls;
    private const int _constZero=0;

    public PlayerController(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerMovement = _playerModel.GetMovement();
        _playerJumping = _playerModel.GetJumpingThings();
        normalControls += Movement;
        normalControls += Jump;
        normalControls += UseFireBall;
        normalControls += UseThunderTorrent;

        actualControls = normalControls;
        GameManager.instance.WinGame += DoWhenGameFinish;
        GameManager.instance.LoseGame += DoWhenGameFinish;


    }
    public void OnUpdate()
    {
        actualControls();
    }
    public void Movement()
    {
        var v = Input.GetAxis(_axisVerticalKey);
        var h = Input.GetAxis(_axisHorizontalKey);
            _playerMovement.Move(h, v);
    }
    public void Jump()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            _playerJumping.Jump();
        }
    }
    private void UseFireBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _playerModel.StartAction1();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _playerModel.MantainAction1();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _playerModel.ExitAction1();
        }
    }
    private void UseThunderTorrent()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _playerModel.StartAction2();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _playerModel.MantainAction2();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _playerModel.ExitAction2();
        }
    }
    public void DoWhenGameFinish()
    {
        actualControls =delegate{ };
    }
}
