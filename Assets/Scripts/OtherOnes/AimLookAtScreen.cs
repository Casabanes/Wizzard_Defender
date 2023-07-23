using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLookAtScreen : MonoBehaviour
{
    private Ray _ray;
    private float _distance;
    [SerializeField] private float _distanceModifier;
    [SerializeField] private Camera _camera;
    void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        _ray = _camera.ScreenPointToRay(screenCenterPoint);
        _distance = Vector3.Distance(transform.position,
        _camera.ScreenToWorldPoint(screenCenterPoint));
        transform.localScale = Vector3.one * _distance* _distanceModifier;
        transform.forward = -_ray.direction;
    }
}
