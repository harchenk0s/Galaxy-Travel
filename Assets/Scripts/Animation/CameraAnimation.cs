using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool test = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeView()
    {
        bool isGameView = _animator.GetBool("IsGameView");
        _animator.SetBool("IsGameView", !isGameView);
    }
}
