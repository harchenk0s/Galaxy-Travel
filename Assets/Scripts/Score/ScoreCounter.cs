using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ScoreCounter : MonoBehaviour
{
    [SerializeField] protected int StartScore = 5000;

    protected float Score;
    protected int NumberCollisions;
    protected ShipEvents ShipEvents;
    protected LevelBuilder LevelBuilder;
    protected bool IsWin = true;

    public FloatEvent ScoreChangeEvent;
    
    public float ScoreProperty
    {
        get { return Mathf.Clamp(Score, 0, float.PositiveInfinity); }
        protected set
        {
            Score = value;
            ScoreChangeEvent.Invoke(ScoreProperty);
        }
    }

    public abstract Score GetScore();

    protected abstract void EndCounting();

    protected abstract void StartCounting();

    protected abstract void Defeat();

    protected virtual void Start()
    {
        LevelBuilder = FindObjectOfType<LevelBuilder>();
        ShipEvents = FindObjectOfType<ShipEvents>();
        LevelBuilder.StartGameEvent.AddListener(StartCounting);
        LevelBuilder.EndLevelEvent.AddListener(EndCounting);
        LevelBuilder.DefeatEvent.AddListener(Defeat);
        ShipEvents.ShipHitEvent.AddListener(ShipHit);
    }

    protected virtual void ShipHit()
    {
        NumberCollisions++;
    }

    private void OnDestroy()
    {
        ScoreChangeEvent.RemoveAllListeners();
    }
}