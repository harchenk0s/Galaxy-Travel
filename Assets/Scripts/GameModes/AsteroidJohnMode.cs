using UnityEngine;

public class AsteroidJohnMode : GameMode
{
    [SerializeField] private GameObject _asteroidShip;
    [SerializeField] private float _duration = 50f;

    private bool _changeMode = false;
    private LevelBuilder _levelBuilder;

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _levelBuilder.ChangeShip(_asteroidShip);
        _levelBuilder.ShipChanged.AddListener(ChangeShipToAsteroid);
        _levelBuilder.GameModeChanged.AddListener(ChangeShipOnDestroy);
        Waves.Add(new Wave(typeof(GridRandomAlg), Strings.AlgorithmsParameters.GridRandomAlg.GridRandomShips, _duration));
    }

    private void ChangeShipToAsteroid(GameObject _)
    {
        _levelBuilder.ShipChanged.RemoveListener(ChangeShipToAsteroid);
        _levelBuilder.ChangeShip(_asteroidShip);
        _levelBuilder.ShipChanged.AddListener(ChangeShipToAsteroid);
    }

    private void ChangeShipOnDestroy(GameObject mode)
    {
        if(mode != gameObject)
        {
            _changeMode = true;
        }
    }

    private void OnDestroy()
    {
        if(_levelBuilder != null)
        {
            _levelBuilder.GameModeChanged.RemoveListener(ChangeShipOnDestroy);
            _levelBuilder.ShipChanged.RemoveListener(ChangeShipToAsteroid);
        }

        if (_changeMode)
        {
            _levelBuilder.ChangeShip(Resources.Load<GameObject>(PlayerPrefs.GetString(Strings.PlayerPrefs.Ship)));
        }
    }
}
