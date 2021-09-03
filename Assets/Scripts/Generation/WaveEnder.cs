using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveEnder : MonoBehaviour
{
    private Vector3 _startPosition;

    public UnityEvent EndWaveEvent;

    public void ReturnToStart()
    {
        EndWaveEvent.Invoke();
        transform.position = _startPosition;
    }

    private void Awake()
    {
        _startPosition = this.transform.position;
    }
}
