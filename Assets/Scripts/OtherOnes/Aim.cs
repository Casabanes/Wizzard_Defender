using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Ray _raycast;
    public LayerMask _aimColliderLayerMask;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform debugingTransform;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKey(KeyCode.Mouse1))
        {
        }
            Aiming();
    }
    private void Aiming()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        _raycast = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(_raycast,out RaycastHit raycastHit, 1000f, _aimColliderLayerMask))
        {
            //player.forward = raycastHit.point;
            debugingTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            player.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }

    }
}
