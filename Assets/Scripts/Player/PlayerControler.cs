using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private int _speedX;
    [SerializeField]
    private int _speedY;
  
   
    [SerializeField]
    private PlayerFunctions _playerFunctions;
    [SerializeField]
    private Blink _blink;
    private float _rotY;
    private const int _constZero = 0;
    private Vector3 _localEulerCamera;
    private float _axisH;
    private float _axisV;
    private string _mouseX = "Mouse X";
    private string _mouseY = "Mouse Y";
    private string _horizontal = "Horizontal";
    private string _vertical = "Vertical";

    private float _minRotation = -90;
    private float _maxRotation = 90;
    private bool _isSprinting;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (_playerFunctions == null)
            _playerFunctions = GetComponent<PlayerFunctions>();
        _localEulerCamera = transform.eulerAngles;
    }

    private void Update()
    {
        MovmentInput();
        RotationInput();
        JumpInput();
        Attack();
        Blinking();
        InvokePortal();
        GoingToEntrance();
    }
    private void InvokePortal()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerFunctions.InvokePortal();
        }
    }
    private void FixedUpdate()
    {
        _playerFunctions.Rotation(_rotY,_localEulerCamera);
        _playerFunctions.Movment(_axisH, _axisV,_isSprinting);
    }
    private void MovmentInput()
    {
        _axisH = Input.GetAxisRaw(_horizontal);
        _axisV = Input.GetAxisRaw(_vertical);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }
    private void RotationInput()
    {
        _rotY = Input.GetAxis(_mouseX) * _speedY * Time.deltaTime;
        _localEulerCamera.x -= Input.GetAxis(_mouseY) * _speedX * Time.deltaTime;
        _localEulerCamera.x = Mathf.Clamp(_localEulerCamera.x, _minRotation, _maxRotation);

    }
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _playerFunctions.Jump();
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _playerFunctions.PrimaryAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _playerFunctions.SecondaryAttack();
        }

    }
    private void Blinking()
    {
        if (Input.GetKeyDown(KeyCode.E) && ((_axisH != _constZero || _axisV!= _constZero)))
        {
            _blink.Dash(_axisH, _axisV);
        }
    }
    private void GoingToEntrance()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _blink.GoToEntrance();
        }
    }
}
