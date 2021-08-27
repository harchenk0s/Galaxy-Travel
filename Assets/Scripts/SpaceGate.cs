using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class SpaceGate : MonoBehaviour
{
    [SerializeField] float _boostPercent = 25f;

    public UnityEvent GateUsedEvent;

    public float BoostPercent => _boostPercent;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody.TryGetComponent<Ship>(out _))
        {
            GateUsedEvent.Invoke();
        }
    }
}
