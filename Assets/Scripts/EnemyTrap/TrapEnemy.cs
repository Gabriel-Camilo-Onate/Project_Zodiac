using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemy : MonoBehaviour
{
    [SerializeField]
    int _playerLayer, _surfSpeed;
    [SerializeField]
    SphereCollider TriggerZone;
    [SerializeField]
    Transform _surPos, _groundPos, _bulletPosition;
    [SerializeField]
    private PlayerControler _player;
    [SerializeField]
    private SpikeMissile _bulletPrefab;
    private SpikeMissile _bulletInstance;
    CapsuleCollider _myCollider;
    Rigidbody _myRb;
    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    float _rotationSpeed;
    Quaternion _lookRotation;
    Vector3 _direction;

    bool dead, isDead, moving;
    Transform _final;
    Animator _myAnimator;
    private bool _hasTriggered;
    [SerializeField]
    private string _damageTrigger;
    private const int _constZero = 0;
    [SerializeField]
    private float _stunTime;
    void Start()
    {
        dead = false;
        isDead = false;
        transform.position = _groundPos.position;
        _myCollider = GetComponent<CapsuleCollider>();
        _myRb = GetComponent<Rigidbody>();
        _myCollider.enabled = false;
        _myRb.useGravity = false;
        _myAnimator = GetComponent<Animator>();
        if (_liveObject==null)
        {
            _liveObject = GetComponent<LiveObject>();
            if (_liveObject == null)
            {
                Debug.LogError("El componente LiveObject no fue encontrado");
            }
        }
        if (_player == null)
        {
            _player = FindObjectOfType<PlayerControler>();
            if (_player == null)
            {
                Debug.LogError("No se encontró al objeto Player");
            }
        }
    }

    private IEnumerator AttackPeriod()
    {
        yield return new WaitForSeconds(2f);
        Attack();
        StartCoroutine(AttackPeriod());
    }

    private void Update()
    {
        if (_liveObject._isDead)
        {
            if (isDead)
            {
                return;
            }
            StopCoroutine(AttackPeriod());
            Die();
        }
        else
        {
            if (!_liveObject._takingDamage)
            {
                _hasTriggered = false;
                    MoveUpDown();
                if (_player != null)
                {
                    Rotate();
                }
            }
            else
            {
                if (!_hasTriggered)
                {
                    StartCoroutine(Stun());
                    StopCoroutine(AttackPeriod());
                    _myAnimator.SetTrigger(_damageTrigger);
                    _hasTriggered = true;
                }
            }
        }
    }
    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(_stunTime);
        _liveObject._takingDamage = false;
    }
    private void Attack()
    {
        //Animacion
        if(isDead == true)
        {
            return;
        }
        _myAnimator.SetTrigger("attack");
    }
    public void Shoot()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<PlayerControler>();
        }
        _bulletInstance = Instantiate(_bulletPrefab, transform.position, _bulletPrefab.transform.rotation);
        _bulletInstance.direcc = new Vector3(_player.transform.position.x - transform.position.x, _constZero, _player.transform.position.z - transform.position.z).normalized;
        _bulletInstance.transform.position = _bulletPosition.position;
        _bulletInstance.transform.rotation = transform.rotation;
    }
    private void Rotate()
    {
        if (isDead == true)
        {
            return;
        }
        _direction = (_player.transform.position - transform.position).normalized;
        _direction.y = _constZero;
        _lookRotation = Quaternion.LookRotation(_direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _rotationSpeed);
    }


    private void Die()
    {
        isDead = true;
        _myCollider.enabled = false;
        _myAnimator.SetTrigger("die");
        transform.position = _final.position;
        StartCoroutine(WaitForDestroy());
    }
    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    //Los TriggerEnter y Exit están para asegurarse que la trampa siempre sigue al jugador y de no ser el caso, no siga nada
   
    public void On(Collider obj)
    {
        if (isDead == true)
        {
            return;
        }
        _final = _surPos;
        moving = true;
        _myCollider.enabled = true;
    }
    public void Off()
    {
        if (isDead == true)
        {
            return;
        }
        _final = _groundPos;
        moving = true;
        _player = null;
        if (_myCollider != null)
        {
        _myCollider.enabled = false;
        }
        if (_myRb != null)
        {
        _myRb.useGravity = false;
        }
        StopAllCoroutines();
    }
    private void MoveUpDown()
    {
        if (_final == null)
        {
            return;
        }
        transform.position += (_final.position - transform.position).normalized * Time.deltaTime * _surfSpeed;
        if(transform.position == _final.position || 
            ((_final.position - transform.position).normalized.y < _constZero 
            && _final.position == _surPos.position) 
            || ((_final.position - transform.position).normalized.y > _constZero 
            && _final.position == _groundPos.position))
        {
            moving = false;
            _myRb.useGravity = true;
            if(_final.position == _surPos.position)
            {
                StartCoroutine(AttackPeriod());
            }
        }
    }
}
