using UnityEngine;

public class AsteroidJohnMode : GameMode
{
    [SerializeField] private GameObject _asteroidShip;
    [SerializeField] private float _duration = 50f;

    private LevelBuilder _levelBuilder;

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _levelBuilder.ChangeShip(_asteroidShip.GetComponent<Ship>());
        Waves.Add(new Wave(typeof(GridRandomAlg), Strings.AlgorithmsParameters.GridRandomAlg.GridRandomShips, _duration));
    }
}
