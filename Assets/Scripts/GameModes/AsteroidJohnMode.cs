using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidJohnMode : GameMode
{
    [SerializeField] GameObject _asteroidShip;
    [SerializeField] float _duration = 50f;

    private LevelBuilder _levelBuidler;

    private void Awake()
    {
        _levelBuidler = FindObjectOfType<LevelBuilder>();
        _levelBuidler.ChangeShip(_asteroidShip);
        _levelBuidler.ChangeShipEvent.AddListener(ChangeShipToAsteroid);
        _waves.Add(new Wave(typeof(GridRandomAlg), "GridRandomShips", _duration));
    }

    private void ChangeShipToAsteroid(GameObject _)
    {
        _levelBuidler.ChangeShipEvent.RemoveListener(ChangeShipToAsteroid);
        _levelBuidler.ChangeShip(_asteroidShip);
        _levelBuidler.ChangeShipEvent.AddListener(ChangeShipToAsteroid);
    }

    private void OnDestroy()
    {
        _levelBuidler.ChangeShip(Resources.Load<GameObject>(PlayerPrefs.GetString("ShipDefault")));
    }
}
