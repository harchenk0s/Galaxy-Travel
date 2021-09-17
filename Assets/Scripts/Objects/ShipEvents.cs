using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipEvents : MonoBehaviour
{
    private Ship _ship = null;

    public FloatEvent ChangedSpeed;
    public IntEvent ChangedArmor;
    public UnityEvent SpeedDowned;
    public UnityEvent SpeedUped;
    public UnityEvent ShipHited;
    public UnityEvent ShipDied;

    public void ChangeShip(GameObject ship)
    {
        _ship = ship.GetComponent<Ship>();
        _ship.ChangedSpeed.AddListener(ChangedSpeed.Invoke);
        _ship.ChangedArmor.AddListener(ChangedArmor.Invoke);
        _ship.SpeedDowned.AddListener(SpeedDowned.Invoke);
        _ship.SpeedUped.AddListener(SpeedUped.Invoke);
        _ship.ShipHited.AddListener(ShipHited.Invoke);
        _ship.ShipDied.AddListener(ShipDied.Invoke);
    }
}
