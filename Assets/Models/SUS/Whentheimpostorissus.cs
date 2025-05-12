using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whentheimpostorissus : MonoBehaviour
{

    [SerializeField] private AudioSource _audiosource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _layerPlayer;
    [SerializeField] private float _waitForDestroy;
    [SerializeField] private float _waitForDestroyTwo;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Transform _particlesPosition;
    [SerializeField] private Rigidbody _player;
    [SerializeField] private PlayerLiveObject _playerLiveObject;
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionUpwardsModifyer;
    [SerializeField] private float _explotionRadius;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private int _damage;
    [SerializeField] private SphereCollider _sphereCollider;


    void Start()
    {
        if (_audiosource == null)
        {
            _audiosource = gameObject.GetComponent<AudioSource>();
        }
        if (_playerLiveObject == null)
        {
            _playerLiveObject = FindObjectOfType<PlayerLiveObject>();
            if (_playerLiveObject == null)
            {
                Debug.LogError("No se encontró al objeto _playerLiveObject");
                return;
            }
            if (_playerLiveObject != null)
            {
                if (_player == null)
                {
                    _player = _playerLiveObject.GetComponent<Rigidbody>();
                }
                if (_player == null)
                {
                    Debug.LogError("No se encontró el componente Rigidbody en el objeto Player");
                    return;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            _audiosource.Play();
            StartCoroutine(DestroySelfPartOne());
            _sphereCollider.enabled = false;
        }
    }
    private IEnumerator DestroySelfPartOne()
    {
        yield return new WaitForSeconds(_waitForDestroy);
        _particles = Instantiate(_particles, _particlesPosition.position, _particlesPosition.rotation);
        _particles.Pause();
        StartCoroutine(DestroySelfPartTwo());
    }
    private IEnumerator DestroySelfPartTwo()
    {
        yield return new WaitForSeconds(_waitForDestroyTwo);
        _player.AddExplosionForce(_explotionForce, _particlesPosition.position,
            _explotionRadius, _explotionUpwardsModifyer, _forceMode);
        _playerLiveObject.TakeDamage(_damage);
        _particles.Play();
        Destroy(gameObject);
    }

}
