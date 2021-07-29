using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    public Rigidbody rbody;
    public float impulseForce;
    public float timeToDeactivate;
    private Vector3 _lastVelocity;
    private float _countdownToDeactivate;

    public UnityEvent OnCollision = default;
    public UnityEvent OnDeactivate = default;

    void Start()
    {
        _countdownToDeactivate = 0;
    }

    void Update()
    {
        _lastVelocity = rbody.velocity;
        CountdownToDeactivate();
    }

    public void SetDirection(Vector3 newDirection)
    {
        rbody.AddForce(newDirection * impulseForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wall") || 
           other.gameObject.CompareTag("Player") || 
           other.gameObject.CompareTag("Ball"))
        {
            var speed = _lastVelocity.magnitude;
            Vector3 collisionNormal = other.contacts[0].normal;
            collisionNormal.z = 0f;
            Vector3 reflectDirection = Vector3.Reflect(_lastVelocity.normalized, collisionNormal);
            reflectDirection.z = 0f;
            rbody.velocity = reflectDirection * Mathf.Max(speed, 0f);  
            OnCollision.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ObjectToBeDestroyed"))
        {
            _countdownToDeactivate = 0;
            gameObject.SetActive(false);
            OnDeactivate.Invoke();
        }
    }

    void CountdownToDeactivate()
    {
        if(_countdownToDeactivate < 1)
        {
            _countdownToDeactivate += Time.deltaTime / timeToDeactivate;
        }
        else
        {
            _countdownToDeactivate = 0;
            gameObject.SetActive(false);
            OnDeactivate.Invoke();
        }
    }
}
