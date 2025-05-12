using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField]
    private int _layerPlayer;
    [SerializeField]
    private float _forceToAdd;
    [SerializeField]
    private ForceMode _forceMode;
    [SerializeField]
    private AudioSource _audioSource;
    private const int _constZero = 0;
    private Rigidbody _player;
    [SerializeField]
    private float _minPitchOfSound;
    [SerializeField]
    private float _maxPitchOfSound;

    private void Start()
    {
        if(_layerPlayer == _constZero)
        {
            Debug.LogError("La variable _layerPlayer no fue asignada");
        }
        if (_forceToAdd == _constZero)
        {
            Debug.LogError("La variable _forceToAdd no fue asignada");
        }
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("No se encontró el componente AudioSource");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerPlayer)
        {
            if (_player == null)
            {
                _player = other.GetComponent<Rigidbody>();
            }
            _audioSource.pitch = Random.Range(_minPitchOfSound, _maxPitchOfSound);
            _audioSource.Play();
            _player.velocity = new Vector3(_player.velocity.x, _constZero, _player.velocity.z);
            _player.AddForce(Vector3.up * _forceToAdd, _forceMode);

        }
    }
}
