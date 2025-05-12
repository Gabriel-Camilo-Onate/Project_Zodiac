using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private FaderManager _faderManager;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _childrenAudioSource;
    [SerializeField]
    private int _layerPlayer;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private Transform _particleSystemPosition;
    [SerializeField]
    private float _timeToChangeScene;
    private void Start()
    {
        if (_faderManager == null)
        {
            _faderManager = FindObjectOfType<FaderManager>();
            if (_faderManager == null)
            {
                Debug.LogError("El objeto FaderManager no se encuentra en la escena");
                return;
            }
        }
        if (_audioSource == null)
        {
            _audioSource = FindObjectOfType<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogError("No se encuentra el componente Audiosource");
                return;
            }
        }
        if (_childrenAudioSource == null)
        {
            _childrenAudioSource = GetComponentInChildren<AudioSource>();
            if (_childrenAudioSource == null)
            {
                Debug.LogError("No se encuentra el componente ChildrenAudiosource");
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer== _layerPlayer)
        {
            _audioSource.Play();
            _particleSystem = Instantiate(_particleSystem, _particleSystemPosition.position, _particleSystemPosition.rotation);
            _particleSystem.transform.parent = _particleSystemPosition;
            collision.gameObject.GetComponent<PlayerFunctions>().InvalidateMovment();
            collision.gameObject.GetComponent<PlayerLiveObject>().enabled = false;
            if (_childrenAudioSource.isPlaying)
            {
                _childrenAudioSource.Stop();
            }
            StartCoroutine(CallFader());
        }
    }
    private IEnumerator CallFader()
    {
        yield return new WaitForSeconds(_timeToChangeScene);
        _faderManager.SceneExit();
    }
    public void SetParticleSystemPosition(Transform position)
    {
        _particleSystemPosition = position;
    }
}
