using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private BasicLiveEntity _liveEntity;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;
    private PlayerView _playerView;
    private PlayerController _playerController;
    void Start()
    {
        _animator = (Animator)GameManager.instance.CheckComponent(_animator, gameObject, typeof(Animator));
        _rigidBody = (Rigidbody)GameManager.instance.CheckComponent(_rigidBody, gameObject, typeof(Rigidbody));
        _playerModel = (PlayerModel)GameManager.instance.CheckComponent(_playerModel, gameObject,typeof(PlayerModel));
        _liveEntity=(BasicLiveEntity)GameManager.instance.CheckComponent(_liveEntity, gameObject, typeof(LiveEntity));
        _playerController = new PlayerController(_playerModel);
        _playerView = new PlayerView().SetAnimator(_animator)
                                      .SetRigidBody(_rigidBody);
        _playerModel.GetMovement().Moving += _playerView.Move;
        _playerModel.GetJumpingThings().Jumping+= _playerView.Jump;
       // _playerModel.shootFireBall += _playerView.Attack01;
        //_playerModel.chanelingSpell += _playerView.Attack02;
    }
    public PlayerView GetPlayerView()
    {
        return _playerView;
    }
    void Update()
    {
        _playerController.OnUpdate();
        _playerView.OnUpdate();
    }
   
}
