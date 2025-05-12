using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionZone : MonoBehaviour
{
    [SerializeField]
    int _playerLayer;
    [SerializeField]
    TrapEnemy _trapenemie;
    private void Start()
    {
        if (_trapenemie == null)
        {
            _trapenemie = GetComponentInParent<TrapEnemy>();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            _trapenemie.On(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            _trapenemie.Off();
        }


    }

}
