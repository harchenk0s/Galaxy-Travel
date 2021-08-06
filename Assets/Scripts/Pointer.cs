using UnityEngine;

public class Pointer : MonoBehaviour
{
    private float _leftBorderX = -10000f;
    private float _rightBorderX = 10000f;
    private PointerBorder[] _borders;

    private void Start()
    {
        _borders = FindObjectsOfType<PointerBorder>();

        if (_borders.Length > 0)
        {
            float leftBorder = float.MaxValue;
            float rightBorder = float.MinValue;

            foreach (PointerBorder border in _borders)
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

    public void Move(float delta)
    {
        float resultX;
        float deltaSum = transform.position.x + delta;

        if (deltaSum > _rightBorderX || deltaSum < _leftBorderX)
        {
            resultX = deltaSum < _leftBorderX ? _leftBorderX : _rightBorderX;
        }
        else
        {
            resultX = deltaSum;
        }

        transform.position = new Vector3(resultX, 0, 0);
    }
}