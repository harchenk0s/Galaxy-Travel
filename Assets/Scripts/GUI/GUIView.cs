using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIView : MonoBehaviour
{
    [SerializeField] Text _scoreField;
    [SerializeField] Slider _armorSlider;
    [SerializeField] Slider _speedSlider;

    private ScoreCounter _scoreCounter;
    private LevelBuilder _levelBuilder;
    private Ship _ship;

    private void Awake()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _ship = FindObjectOfType<Ship>();
        _levelBuilder.ChangeGameModeEvent.AddListener(OnChangeScoreCounter);
        _levelBuilder.ChangeShipEvent.AddListener(OnChangeShip);
        _levelBuilder.StartGameEvent.AddListener(InitializeFields);
    }

    private void InitializeFields()
    {
        _speedSlider.value = _ship.CurrentPercentSpeed / 100;
        _armorSlider.maxValue = _ship.MaxArmor;
        _armorSlider.value = _ship.CurrentArmor;
        _ship.ChangeArmorEvent.AddListener(ChangeArmor);
        _ship.ChangeSpeedEvent.AddListener(ChangeSpeed);
    }

    private void OnChangeShip(GameObject ship)
    {
        _ship = ship.GetComponent<Ship>();
        InitializeFields();
    }

    private void OnChangeScoreCounter(GameObject gameMode)
    {
        _scoreCounter = gameMode.GetComponent<ScoreCounter>();
        _scoreCounter.ScoreChangeEvent.AddListener(ChangeScore);
    }

    private void ChangeScore(float value)
    {
        _scoreField.text = ((int)value).ToString();
    }

    private void ChangeArmor(int value)
    {
        _armorSlider.value = value;
    }

    private void ChangeSpeed(float value)
    {
        _speedSlider.value = _ship.CurrentPercentSpeed / 100;
    }
}
