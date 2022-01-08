using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSpinAttack : MonoBehaviour
{
    public bool IsSpin { get; private set; }

    private bool _canSpin;

    [SerializeField] private float SpinTime, CanSpinTime;
    void Start()
    {
        _canSpin = true;
        IsSpin = false;
    }

    
    void Update()
    {
        PlayerSpinAttack();
    }

    void PlayerSpinAttack() 
    {
        if (Input.GetKeyDown(KeyCode.X) && !IsSpin && _canSpin)
        {
            StartCoroutine(Spin());
        }
    }

    private IEnumerator Spin()
    {
        _canSpin = false;
        IsSpin = true;
        yield return new WaitForSeconds(SpinTime);

        IsSpin = false;
        yield return new WaitForSeconds(CanSpinTime);
        _canSpin = true;

    }
}
