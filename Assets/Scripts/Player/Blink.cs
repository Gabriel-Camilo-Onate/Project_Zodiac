using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _audioClips;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private Transform _particleSystemPosition;
    [SerializeField]
    private Vector3 _direction;
    [SerializeField]
    private float _distanceMultiplier;
    [SerializeField]
    private float _minPitchInSound;
    [SerializeField]
    private float _maxPitchInSound;
    private RaycastHit _rayCast;
    private const int _constZero = 0;
    private const int _constOne = 1;
    [SerializeField]
    private float _blinkCooldown;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _minfov;
    [SerializeField]
    private float _maxfov;
    [SerializeField]
    private float _fovPlusAmount;
    private bool _isChangingFOV;

    private bool _hasBlinked;
    [SerializeField]
    private Transform _entrance;
    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("No se encontró el componente Audiosource");
                return;
            }        
        }
        if (_particleSystem == null)
        {
            Debug.LogError("No se asignó la variable _particleSystem");
        }
        if (_audioClips==null)
        {
            Debug.LogError("No se asignó la variable _audioClips");
        }
        if (_particleSystemPosition == null)
        {
            Debug.LogError("No se asignó la variable _particleSystemPosition");
            return;
        }
    }
    private void Update()
    {
        CameraFOVChange();
    }
    public void Dash(float x, float z)
    {
        if (_hasBlinked)
        {
            return;
        }
        else
        {
            _direction = new Vector3(x, _constZero,z);
            if (Physics.Raycast(transform.position, ((transform.forward * _direction.z)
                + (transform.right * _direction.x)) * _distanceMultiplier, 
                out _rayCast, _distanceMultiplier))
            {
                Feedback();
                transform.position = _rayCast.point;
            }
            else
            {
                Feedback();
                transform.position += ((transform.forward * _direction.z) + (transform.right * _direction.x)) *
                _distanceMultiplier;
            }
            _camera.fieldOfView = _minfov;
            _isChangingFOV = true;
            StartCoroutine(Cooldown());
        }
    }
    public void ResetHasBlinked()
    {
        _hasBlinked = false;
    }
    private void CameraFOVChange()
    {
        if (!_isChangingFOV)
        {
            return;
        }
        else
        {
            if (_camera.fieldOfView > _maxfov)
            {
                _camera.fieldOfView = _maxfov;
                _isChangingFOV = false;
            }
            else
            {
                _camera.fieldOfView += _fovPlusAmount * Time.deltaTime;
            }
        }
    } 
    private void Feedback()
    {
        _audioSource.clip = _audioClips[Random.Range(_constZero, _audioClips.Length)];
        _audioSource.pitch = Random.Range(_minPitchInSound, _maxPitchInSound);
        _audioSource.Play();
        Instantiate(_particleSystem, _particleSystemPosition.position, _particleSystemPosition.rotation)
            .transform.parent=_particleSystemPosition;
    }
    private IEnumerator Cooldown()
    {
        _hasBlinked = true;
        yield return new WaitForSeconds(_blinkCooldown);
        _hasBlinked = false;
    }
    public void GoToEntrance()
    {
        if (_entrance != null)
        {
            _hasBlinked = false;
            transform.position = _entrance.position;
        Dash(_constZero, _constZero);
        }
    }
}
