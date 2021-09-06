using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipEvents : MonoBehaviour
{
    private Ship _ship = null;

    public FloatEvent ChangeSpeedEvent;
    public IntEvent ChangeArmorEvent;
    public UnityEvent SpeedDownEvent;
    public UnityEvent SpeedUpEvent;
    public UnityEvent ShipHitEvent;
    public UnityEvent ShipDeadEvent;

    public void ChangeShip(GameObject ship)
    {
        _ship = ship.GetComponent<Ship>();
        _ship.ChangeSpeedEvent.AddListener(ChangeSpeedEvent.Invoke);
        _ship.ChangeArmorEvent.AddListener(ChangeArmorEvent.Invoke);
        _ship.SpeedDownEvent.AddListener(SpeedDownEvent.Invoke);
        _ship.SpeedUpEvent.AddListener(SpeedUpEvent.Invoke);
        _ship.ShipHitEvent.AddListener(ShipHitEvent.Invoke);
        _ship.ShipDeadEvent.AddListener(ShipDeadEvent.Invoke);
    }
}
