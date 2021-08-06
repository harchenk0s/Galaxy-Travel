using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject ObjectOfControl;

    private float _leftBorderX = -10000f;
    private float _rightBorderX = 10000f;

    private void Awake()
    {
        PointerBorder[] borders = FindObjectsOfType<PointerBorder>();

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

    //private 
    //private float x;
    //private float y;

    //public float X
    //{
    //    get { return x; }
    //    set { }
    //}
}
