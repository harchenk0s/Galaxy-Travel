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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Opened() => OpenEvent.Invoke();
    private void Closed() => CloseEvent.Invoke();

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _animator.SetTrigger("CloseTrigger");
    }
}
