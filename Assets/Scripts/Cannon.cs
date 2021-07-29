using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour
{
    public Transform crosshair;
    public Transform cannonMouth; 
    public bool canShoot = true;
    private Vector3 _direction;

    public UnityEvent OnShoot = default;

    void Update()
    {
        Inputs();
        GetDirection();
    }
    void Inputs()
    {
        if (Input.touchCount == 1)
        {
            canShoot = true;
            PositionCrosshair();
        }
        else if(Input.touchCount == 2 && canShoot && !GameManager.instance.endGame)
        {
            canShoot = false;
            Ball newBall = GameManager.GetPool().TryToGetBall();
            newBall.rbody.velocity = Vector3.zero;
            newBall.transform.position = cannonMouth.position;
            newBall.SetDirection(_direction);
            GameManager.instance.amountShoots++;
            OnShoot.Invoke();
        }
    }

    void GetDirection()
    {
        _direction = (crosshair.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }

    void PositionCrosshair()
    {
        Touch currentTouch = Input.GetTouch(0); 

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(currentTouch.position);
        newPosition.z = 0f;
        crosshair.position = newPosition;
    }
}