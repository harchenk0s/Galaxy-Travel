using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Ship _ship = null;
    private PointerBorder[] _borders;
    private IEnumerator _returnToShip = null;

    public Vector2 _minBorders { get; private set; }
    public Vector2 _maxBorders { get; private set; }

    public void StartMove()
    {
        if (_returnToShip != null)
        {
            StopCoroutine(_returnToShip);
            _returnToShip = null;
            transform.position = _ship.transform.position;
        }
    }

    public void Move(Vector2 delta)
    {
        float deltaSumX = transform.position.x + delta.x;
        float deltaSumY = transform.position.y + delta.y;

        float resultX = Mathf.Clamp(deltaSumX, _minBorders.x, _maxBorders.x);
        float resultY = Mathf.Clamp(deltaSumY, _minBorders.y, _maxBorders.y);

        transform.position = new Vector3(resultX, resultY, 0);
    }

    public void EndMove()
    {
        _returnToShip = ReturnToShipCourutine();
        StartCoroutine(_returnToShip);
    }

    private void Awake()
    {
        _minBorders = new Vector2(-10000f, -10000f);
        _maxBorders = new Vector2(10000f, 10000f);
        _ship = FindObjectOfType<Ship>();
    }

    private void Start()
    {
        if(_ship == null)
        {
            enabled = false;
            throw new UnityException("Ship not found");
        }
            
        InitializeBorders();
    }

    private IEnumerator ReturnToShipCourutine()
    {
        while (transform.position != _ship.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _ship.transform.position, 0.5f);
            yield return null;
        }
        _returnToShip = null;
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

            _minBorders = new Vector2(leftBorder, downBorder);
            _maxBorders = new Vector2(rightBorder, upBorder);
        }
    }
}