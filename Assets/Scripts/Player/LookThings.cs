using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookThings : MonoBehaviour
{
    [SerializeField] private PlayerModel _model;
    private ThirdPersonMovment _movement;
    private const int _constZero = 0;
    private const int _constOne = 1;
    private Vector3 _direction;
    [SerializeField] private Transform _orientationObject;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _time;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _timeValue;
    [SerializeField] private float _timeToInterpolate = 1;
    [SerializeField] private float _realTimeToInterpolate;

    public LookThings SetOrientationObject(Transform orientationObject)
    {
        _orientationObject = orientationObject;
        return this;
    }
    void Start()
    {
        if (_model == null)
        {
            _model = GetComponent<PlayerModel>();
            if (_model == null)
            {
                Debug.Log("Model not found");
                gameObject.SetActive(false);
            }
        }
        if (_movement == null)
        {
            _movement = _model.GetMovement();
        }
            _movement.Moving += LookAtForward;
    }
    public void LookAtForward(float x, float z)
    {
        if (x == 0 && z == 0)
        {
            _time = _constZero;
            _timeValue = _constZero;
        }
        TimeToInterpolate();
        _direction.x = _orientationObject.forward.x;
        _direction.z = _orientationObject.forward.z;
        _direction.y = transform.forward.y;
        if (_time < _realTimeToInterpolate)
        {
            _time += Time.deltaTime;
            _timeValue = _time / _realTimeToInterpolate;
            Mathf.Clamp(_time, _constZero, _constOne);
            transform.forward = Vector3.Slerp(transform.forward, _direction, _timeValue);
        }
        else
        {
            _time = _constZero;
            _timeValue = _constZero;
        }
    }
    private void TimeToInterpolate()
    {
        _realTimeToInterpolate = _timeToInterpolate * Vector3.Angle(_direction, transform.forward);
    }
}
