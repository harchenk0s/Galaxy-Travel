using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject ObjectOfControl;

    private float _leftBorderX = -10000f;
    private float _rightBorderX = 10000f;
    private float x;

    public PointerBorder[] borders;

    public float X
    {
        get { return transform.position.x; }
        set
        {
            if (value > _leftBorderX && value <= _rightBorderX)
            {
                transform.position = new Vector3(value, 0, 0);
            }

            if (transform.position.x > _rightBorderX)
                transform.position = new Vector3(_rightBorderX, 0, 0);

            if (transform.position.x < _leftBorderX)
                transform.position = new Vector3(_leftBorderX, 0, 0);
                
        }
    }

    private void Start()
    {
        borders = FindObjectsOfType<PointerBorder>();

        if (borders.Length != 0)
        {
            float leftBorder = float.MaxValue;
            float rightBorder = float.MinValue;

            foreach (PointerBorder border in borders)
            {
                float borderX = border.transform.position.x;

                if (borderX < leftBorder)
                    leftBorder = borderX;

                if (borderX > rightBorder)
                    rightBorder = borderX;
            }

            _leftBorderX = leftBorder;
            _rightBorderX = rightBorder;
        }
        
    }
}
