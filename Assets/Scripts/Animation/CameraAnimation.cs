using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;

    public void ChangeView()
    {
        bool isGameView = _animator.GetBool("IsGameView");
        _animator.SetBool("IsGameView", !isGameView);
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
