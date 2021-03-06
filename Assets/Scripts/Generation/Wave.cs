using System;
using UnityEngine;

public class Wave
{
    private Type _algorithm;
    private string _parameters;
    private float _duration;
    private Generator _generator;

    public bool IsWaveEnd { get; private set; } = false;

    public Wave(Type algorithm, string parameters, float duration)
    {
        _algorithm = algorithm;
        _parameters = parameters;
        _duration = duration;
    }

    public void StartWave(float speedCorrectionPercent, Generator generator)
    {
        IsWaveEnd = false;
        _generator = generator;
        _duration += _duration * (100 - speedCorrectionPercent) / 100;
        PlayerPrefs.SetString(_algorithm.ToString(), _parameters);
        PlayerPrefs.Save();
        _generator.EndWaveEvent.AddListener(EndWave);
        _generator.ChangeAlgorithm(_algorithm);
        _generator.GenerateWave(_duration);
    }

    private void EndWave()
    {
        IsWaveEnd = true;
        _generator.EndWaveEvent.RemoveListener(EndWave);
    }
}
