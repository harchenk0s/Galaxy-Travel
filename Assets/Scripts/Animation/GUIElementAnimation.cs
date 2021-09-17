using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class GUIElementAnimation : MonoBehaviour
{
    private Animator _animator;

    public UnityEvent OpenEvent;
    public UnityEvent CloseEvent;

    public void Opened() => OpenEvent.Invoke();
    public void Closed() => CloseEvent.Invoke();

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _animator.SetTrigger(Strings.Animation.CloseTrigger);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
