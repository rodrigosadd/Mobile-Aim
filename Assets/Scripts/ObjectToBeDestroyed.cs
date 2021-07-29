using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectToBeDestroyed : MonoBehaviour
{
    public UnityEvent OnDeactivate = default;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            gameObject.SetActive(false);            
            OnDeactivate.Invoke();
        }
    }
}
