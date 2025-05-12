using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    private const int _constZero = 0;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _movmentSpeed;
    [SerializeField]
    private float _sprintSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private JumpBoolSetter _jumpBoolSetter;
    [SerializeField]
    private float _jumpCooldDown=1;
    [SerializeField]
    private bool _jumpCoolDownbool;
    [SerializeField]
    private Animator _animatorController;
    [SerializeField]
    private string _primaryAttack = "_attack";
    [SerializeField]
    private string _secondaryAttack = "_attack2";
    [SerializeField]
    private VaraState _varaState;
    [SerializeField]
    private PrimaryAttack _shoot;
    [SerializeField]
    private int _originalDamage;
    [SerializeField]
    private int _actualDamage;
    [SerializeField]
    private Transform _shootSpawnPoint;
    public float _attackCoolDown=1;
    [SerializeField]
    private AudioClip _attackSound;
    [SerializeField] int _buffDamange = 100;
    public bool _isBuff;



    private bool _attackIsInCoolDown; 
    private float _currentAttackCoolDown;
    private bool _canJump;
    private float _jumpCoolDownTime;

    private RaycastHit _rayCast;
    private bool _canInvokeThePortal;
    [SerializeField]
    private float _distanceMultiplier;
    [SerializeField]
    private Portal _portal;
    [SerializeField]
    private Transform _particleSystemPosition;
    [SerializeField]
    private Animator _invokePortalInstructions;
    private bool _canMove;
    private RaycastHit _rayCastMovment;
    [SerializeField]
    private Transform _rayCastMovmentOriginOffset;
    private Vector3 _directionMovmentRayCast;
    private float _distanceOfRayCastMovment;
    [SerializeField]
    private LayerMask _layerToRayCast;
    void Start()
    {
        if (_camera == null)
        {
            _camera = GetComponentInChildren<Camera>();
        }
        if (_rigidBody == null)
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        if (_jumpBoolSetter == null)
        {
            _jumpBoolSetter = GetComponentInChildren<JumpBoolSetter>();
        }
        if (_animatorController == null)
        {
            _animatorController = GetComponentInChildren<Animator>();
        }
        if (_varaState == null)
        {
            _varaState = GetComponentInChildren<VaraState>();
        }
        _actualDamage = _originalDamage;
        _jumpCoolDownbool = true;
        _canMove = true;
    }

    void Update()
    {
        JumpCoolDown();
        AttackCoolDown();
    }
    public void Movment(float x, float z, bool isSprinting)
    {
        if (_canMove)
        {
            _directionMovmentRayCast = (transform.forward * z) + (transform.right * x).normalized;
            if (isSprinting)
            {
                _rigidBody.velocity =new Vector3(_directionMovmentRayCast.x * _sprintSpeed,_rigidBody.velocity.y, _directionMovmentRayCast.z*_sprintSpeed);
               /* _distanceOfRayCastMovment = _sprintSpeed * Time.deltaTime;
                if (Physics.Raycast(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, out _rayCastMovment, _distanceOfRayCastMovment, _layerToRayCast))
                {
                    _rigidBody.MovePosition(_rayCastMovment.point - _directionMovmentRayCast * _distanceOfRayCastMovment);
                    Debug.DrawRay(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, Color.red, _distanceOfRayCastMovment);

                }
                else
                {
                    Debug.DrawRay(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, Color.red, _distanceOfRayCastMovment);
                    _rigidBody.MovePosition(transform.position + _directionMovmentRayCast * _distanceOfRayCastMovment);
                }*/
            }
            else
            {
                _rigidBody.velocity = new Vector3(_directionMovmentRayCast.x * _movmentSpeed, _rigidBody.velocity.y, _directionMovmentRayCast.z * _movmentSpeed);

                /*
                _distanceOfRayCastMovment = _movmentSpeed * Time.deltaTime;
                if (Physics.Raycast(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, out _rayCastMovment, _distanceOfRayCastMovment, _layerToRayCast))
                {
                    _rigidBody.MovePosition(_rayCastMovment.point - _directionMovmentRayCast * _distanceOfRayCastMovment);
                    Debug.DrawRay(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, Color.red, _distanceOfRayCastMovment);
                }
                else
                {
                    Debug.DrawRay(_rayCastMovmentOriginOffset.position, _directionMovmentRayCast, Color.red, _distanceOfRayCastMovment);
                    _rigidBody.MovePosition(transform.position + _directionMovmentRayCast * _distanceOfRayCastMovment);
                }*/
            }
        }
    }
    public void Rotation(float _rotY, Vector3 _localEulerCamera)
    {
        transform.Rotate(_constZero, _rotY, _constZero);
        _camera.transform.localEulerAngles = _localEulerCamera;
    }
    public void Jump()
    {
        if (_canJump && !_jumpCoolDownbool)
        {
            if (_jumpBoolSetter._touchingFloor)
            {
                _rigidBody.AddForce(transform.up* _jumpForce,ForceMode.Impulse);
                _canJump = false;
                _jumpCoolDownbool = true;
            }
        }
    }
    private void JumpCoolDown() 
    {
        if (!_jumpCoolDownbool)
        {
            return;
        }
        _jumpCoolDownTime += Time.deltaTime;
        if (_jumpCoolDownTime > _jumpCooldDown)
        {
            _canJump = true;
            _jumpCoolDownbool = false;
            _jumpCoolDownTime = _constZero;
        }
    }
    public void PrimaryAttack()
    {
        if (_varaState._isShoting || _attackIsInCoolDown)
        {
            return;
        }
        else
        {
            _animatorController.SetTrigger(_primaryAttack);
            _varaState._isShoting = true;
            _attackIsInCoolDown = true;
            if (!_isBuff)
            {
                _shoot._isBuff = false;
                _shoot._damage = _actualDamage;
            }
            else
            {
                _shoot._isBuff = true;
                _shoot._damage = _buffDamange;
            }
            Instantiate(_shoot, _shootSpawnPoint.position, _shootSpawnPoint.rotation);
        }
    }
    private void AttackCoolDown()
    {
        if (!_attackIsInCoolDown)
        {
            return;
        }
        _currentAttackCoolDown += Time.deltaTime;
        if (_currentAttackCoolDown > _attackCoolDown)
        {
            _currentAttackCoolDown = _constZero;
            _attackIsInCoolDown = false;
        }
    }
    public void SecondaryAttack()
    {
        if (!_varaState._isShoting)
        {
            _animatorController.SetTrigger(_secondaryAttack);
            _varaState._isShoting = true;
        }
        else
        {
            return;
        }
    }
    public IEnumerator DamageBuffed(int _damage, float _buffDuration)
    {
        _actualDamage = _damage;
        yield return new WaitForSeconds(_buffDuration);
        _actualDamage = _originalDamage;
    }
    public void InvokePortal()
    {
        if (!_canInvokeThePortal) 
        {
            return;
        }
        else
        {
            _portal.SetParticleSystemPosition(_particleSystemPosition);
            if (Physics.Raycast(transform.position, transform.forward * _distanceMultiplier,
               out _rayCast, _distanceMultiplier))
            {
                Instantiate(_portal, _rayCast.point, transform.rotation);
            }
            else
            {
                Instantiate(_portal, transform.position+transform.forward*_distanceMultiplier, transform.rotation);
            }
            _canInvokeThePortal = false;
            _invokePortalInstructions.enabled = false;
        }
    }
    public void SetCanInvokeThePortal(bool value)
    {
        _canInvokeThePortal = value;
        _invokePortalInstructions.enabled = true;
    }
    public void InvalidateMovment()
    {
        _canMove = false;
    }
}
