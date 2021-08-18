using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerBorders : MonoBehaviour
{
    [SerializeField] private List<GameObject> _borderPoints = new List<GameObject>(2);

    public Vector2 MinBorders { get; private set; } = Vector2.zero;
    public Vector2 MaxBorders { get; private set; } = Vector2.zero;

    private void Awake()
    {
        InitializeBorders();
    }

    private void InitializeBorders()
    {
        if(_borderPoints.Count > 1)
        {
            float leftBorder = float.MaxValue;
            float rightBorder = float.MinValue;
            float downBorder = float.MaxValue;
            float upBorder = float.MinValue;

            foreach (GameObject border in _borderPoints)
            {
                float borderX = border.transform.position.x;
                float borderY = border.transform.position.y;

                if (borderX < leftBorder)
                    leftBorder = borderX;

                if (borderX > rightBorder)
                    rightBorder = borderX;

                if (borderY < downBorder)
                    downBorder = borderY;

                if (borderY > upBorder)
                    upBorder = borderY;
            }

            MinBorders = new Vector2(leftBorder, downBorder);
            MaxBorders = new Vector2(rightBorder, upBorder);
        }
    }
}
