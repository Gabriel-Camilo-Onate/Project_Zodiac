using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpToEntrance : MonoBehaviour
{
    [SerializeField]
    private Transform _entrance;
    [SerializeField]
    private Blink _player;
    private const int _constZero = 0;
    [SerializeField]
    private int _layerPlayer;

    private void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Blink>();
            if (_player == null)
            {
                Debug.LogError("No se pudo encontrar al objeto Player");
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _layerPlayer)
        {
            return;
        }
        _player.transform.position = _entrance.position;
        if (_player == null)
        {
            _player = other.GetComponent<Blink>();
        }
        _player.ResetHasBlinked();
        _player.Dash(_constZero, _constZero);
    }
}
