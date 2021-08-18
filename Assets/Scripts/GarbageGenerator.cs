using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour
{
    private Pool _pool;
    private Vector2 _minBorders;
    private Vector2 _maxBorders;

    private void Awake()
    {
        _pool = FindObjectOfType<Pool>();
    }

    private void Start()
    {
        PointerBorders pointerBorders = FindObjectOfType<PointerBorders>();

        if (pointerBorders != null)
        {
            _minBorders = pointerBorders.MinBorders;
            _maxBorders = pointerBorders.MaxBorders;
        }
        else
        {
            throw new UnityException("Add PointerBorders on scene!");
        }
    }
}
