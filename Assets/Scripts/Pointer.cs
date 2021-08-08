using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Ship _ship = null;

    private float _leftBorderX = -10000f;
    private float _rightBorderX = 10000f;
    private float _upBorderY = 10000f;
    private float _downBorderY = -10000f;
    private PointerBorder[] _borders;

    private void Awake()
    {
        _ship = FindObjectOfType<Ship>();
        _borders = FindObjectsOfType<PointerBorder>();
    }

    private void Start()
    {
        InitializeBorders();
    }

    public void Move(Vector2 delta)
    {
        float resultX;
        float resultY;
        float deltaSumX = transform.position.x + delta.x;
        float deltaSumY = transform.position.y + delta.y;

        if (deltaSumX > _rightBorderX || deltaSumX < _leftBorderX)
        {
            resultX = deltaSumX < _leftBorderX ? _leftBorderX : _rightBorderX;
        }
        else
        {
            resultX = deltaSumX;
        }

        if (deltaSumY > _upBorderY || deltaSumY < _downBorderY)
        {
            resultY = deltaSumY < _downBorderY ? _downBorderY : _upBorderY;
        }
        else
        {
            resultY = deltaSumY;
        }

        transform.position = new Vector3(resultX, resultY, 0);
    }

    public void ResetPointer()
    {
        if (_ship != null)
            transform.position = _ship.transform.position;
    }

    private void InitializeBorders()
    {
        _borders = FindObjectsOfType<PointerBorder>();

        if (_borders.Length > 1)
        {
            float leftBorder = float.MaxValue;
            float rightBorder = float.MinValue;
            float downBorder = float.MaxValue;
            float upBorder = float.MinValue;

            foreach (PointerBorder border in _borders)
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

            _leftBorderX = leftBorder;
            _rightBorderX = rightBorder;
            _upBorderY = upBorder;
            _downBorderY = downBorder;
        }
    }
}