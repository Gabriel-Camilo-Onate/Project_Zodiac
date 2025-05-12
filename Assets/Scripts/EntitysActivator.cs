using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitysActivator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    private Alceic _alceic;
    [SerializeField]
    private Jabali_Enemy _jabaliEnemy;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private pinchsColliders _pinchsColliders;
    [SerializeField]
    private Animation _animation;
    [SerializeField]
    private Spikes _spikes;
    [SerializeField]
    private int _activarorLayer = 13;
    [SerializeField]
    public bool _isActive;
    [SerializeField]
    private GameObject[] _childs;
    const int _constZero = 0;
    void Start()
    {
        if (_rigidBody == null)
        {
            if (GetComponent<Rigidbody>() != null)
            {
                _rigidBody = GetComponent<Rigidbody>();
            }
        }
        if (_liveObject == null)
        {
            if (GetComponent<LiveObject>() != null)
            {
                _liveObject = GetComponent<LiveObject>();
            }
        }
        if (_alceic == null)
        {
            if (GetComponent<Alceic>() != null)
            {
                _alceic = GetComponent<Alceic>();
            }
        }
        if (_jabaliEnemy == null)
        {
            if (GetComponent<Jabali_Enemy>() != null)
            {
                _jabaliEnemy = GetComponent<Jabali_Enemy>();
            }
        }
        if (_audioSource == null)
        {
            if (GetComponent<AudioSource>() != null)
            {
                _audioSource = GetComponent<AudioSource>();
            }
        }
        if (_animator == null)
        {
            if (GetComponent<Animator>() != null)
            {
                _animator = GetComponent<Animator>();
            }
        }
        if (_pinchsColliders == null)
        {
            if (GetComponent<pinchsColliders>() != null)
            {
                _pinchsColliders = GetComponent<pinchsColliders>();
            }
        }
        if (_spikes == null)
        {
            if (GetComponent<Spikes>() != null)
            {
                _spikes = GetComponent<Spikes>();
            }
        }
        if ( _childs == null)
        {
            if(GetComponentsInChildren<Transform>() != null)
            {
                _childs = GetComponentsInChildren<GameObject>();
            }
        }
        if (_isActive)
        {
            ComponentsOn();
        }
        else
        {
            ComponentsOff();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _activarorLayer)
        {
            ComponentsOn();
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (_isActive)
        {
            return;
        }
        else
        {
            if (collision.gameObject.layer == _activarorLayer)
            {
                ComponentsOn();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == _activarorLayer)
        {
            ComponentsOff();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == _activarorLayer)
        {
            ComponentsOn();
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if(_isActive)
        {
            return;
        }
        else
        {
            if(collision.gameObject.layer==_activarorLayer)
            {
                ComponentsOn();
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.layer== _activarorLayer)
        {
            ComponentsOff();
        }
    }


    private void ComponentsOn()
    {
        if (_rigidBody != null)
        {
            _rigidBody.isKinematic = false;
        }
        if (_liveObject != null)
        {
            _liveObject.enabled = true;
        }
        if (_alceic != null)
        {
            _alceic.enabled = true;
        }
        if (_jabaliEnemy != null)
        {
            _jabaliEnemy.enabled = true;
        }
        if (_audioSource != null)
        {
            _audioSource.enabled = true;
        }
        if (_animator != null)
        {
            _animator.enabled = true;
        }
        if (_pinchsColliders != null)
        {
            _pinchsColliders.enabled = true;
        }
        if (_animation != null)
        {
            _animation.enabled = true;
        }
        if (_spikes != null)
        {
            _spikes.enabled = true;
        }
        if (_childs != null)
        {
            for (int count=_constZero; count <_childs.Length; count++)
            {
                _childs[count].SetActive(true);
            }
        }
        _isActive = true;
    }
    private void ComponentsOff()
    {
        if (_rigidBody != null)
        {
            _rigidBody.isKinematic = true;
        }
        if (_liveObject != null)
        {
            _liveObject.enabled = false;
        }
        if (_alceic != false)
        {
            _alceic.enabled = false;
        }
        if (_jabaliEnemy != null)
        {
            _jabaliEnemy.enabled = false;
        }
        if (_audioSource != null)
        {
            _audioSource.enabled = false;
        }
        if (_animator != null)
        {
            _animator.enabled = false;
        }
        if (_pinchsColliders != null)
        {
            _pinchsColliders.enabled = false;
        }
        if (_animation != null)
        {
            _animation.enabled = false;
        }
        if (_spikes != null)
        {
            _spikes.enabled = false;
        }
        if (_childs != null)
        {
            for (int count = _constZero; count < _childs.Length; count++)
            {
                _childs[count].SetActive(false);
            }
        }
        _isActive = false;
    }
}
