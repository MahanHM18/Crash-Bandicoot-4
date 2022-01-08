using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashAnimation : MonoBehaviour
{
    private Animator _anim;
    private CrashMovement _crash;
    private CrashSpinAttack _spin;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _crash = transform.parent.GetComponent<CrashMovement>();
        _spin = transform.parent.GetComponent<CrashSpinAttack>();
    }

    void Update()
    {
        _anim.SetBool("IsRunning", _crash.IsMove);
        _anim.SetBool("IsFalling", _crash.IsFalling);
        _anim.SetBool("IsGrounded", _crash.IsGrounded);
        _anim.SetBool("IsSpining", _spin.IsSpin);
    }
}
