using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ScoreCounter : MonoBehaviour
{
    [SerializeField] protected int _startScore = 5000;

    protected float _score;
    protected int _numberCollisions;
    protected ShipEvents _shipEvents;
    protected LevelBuilder _levelBuilder;
    protected bool _isWin = true;

    public FloatEvent ScoreChangeEvent;
    
    public float Score
    {
        get { return Mathf.Clamp(_score, 0, float.PositiveInfinity); }
        protected set
        {
            _score = value;
            ScoreChangeEvent.Invoke(Score);
        }
    }

    public abstract Score GetScore();

    protected abstract void EndCounting();

    protected abstract void StartCounting();

    protected abstract void Defeat();

    protected virtual void Start()
    {
        _levelBuilder = FindObjectOfType<LevelBuilder>();
        _shipEvents = FindObjectOfType<ShipEvents>();
        _levelBuilder.StartGameEvent.AddListener(StartCounting);
        _levelBuilder.EndLevelEvent.AddListener(EndCounting);
        _levelBuilder.DefeatEvent.AddListener(Defeat);
        _shipEvents.ShipHitEvent.AddListener(ShipHit);
    }

    protected virtual void ShipHit()
    {
        _numberCollisions++;
    }

    private void OnDestroy()
    {
        ScoreChangeEvent.RemoveAllListeners();
    }
}