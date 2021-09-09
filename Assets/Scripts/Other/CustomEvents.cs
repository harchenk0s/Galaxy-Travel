using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }
[Serializable]
public class FloatEvent : UnityEvent<float> { }
[Serializable]
public class IntEvent : UnityEvent<int> { }
