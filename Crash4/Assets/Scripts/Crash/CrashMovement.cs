using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashMovement : MonoBehaviour
{
    [SerializeField] private float RunSpeed, RotateRate, JumpHeight;
    [HideInInspector] public float Speed;


    private Rigidbody _rb;
    private Vector3 _moveDir;

    private float horizontal, vertical;
    private CrashSpinAttack _spin;

    public bool IsMove { get { return horizontal != 0 || vertical != 0; } }
    public bool IsFalling { get { return _rb.velocity.y < 0 && !IsGrounded; } }
    public bool IsGrounded { get { return Physics.CheckSphere(GroundPoint.position, GroundDistance, GroundLayer); } }

    private bool dubleJump;

    [SerializeField] private Transform GroundPoint;
    [SerializeField] private float GroundDistance;
    [SerializeField] private LayerMask GroundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _spin = GetComponent<CrashSpinAttack>();
    }
    void Start()
    {
        Speed = RunSpeed;
        dubleJump = false;
    }


    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        _moveDir = Camera.main.transform.forward * vertical + Camera.main.transform.right * horizontal;
        _rb.velocity = new Vector3(_moveDir.x * RunSpeed, _rb.velocity.y, _moveDir.z * RunSpeed);

        PlayerRotate();
        PlayerJump();

        if (_moveDir.x == 0 && _moveDir.y == 0)
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void PlayerRotate()
    {
        if (IsMove)
            _rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), Time.deltaTime * RotateRate);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (IsGrounded)
            {
                if (!_spin.IsSpin)
                    transform.GetChild(0).GetComponent<Animator>().SetTrigger("Jump");
                _rb.velocity = new Vector3(_rb.velocity.x, JumpHeight, _rb.velocity.z);
            }

            if (!IsGrounded && !dubleJump)
            {
                dubleJump = true;
                if (!_spin.IsSpin)
                    transform.GetChild(0).GetComponent<Animator>().SetTrigger("Jump");
                _rb.velocity = new Vector3(_rb.velocity.x, JumpHeight, _rb.velocity.z); 
            }

        }
        if (IsGrounded)
            dubleJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GroundPoint.position, GroundDistance);
    }
}
