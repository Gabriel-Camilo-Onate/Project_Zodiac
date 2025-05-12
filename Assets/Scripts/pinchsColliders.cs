using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchsColliders : MonoBehaviour
{
    private PlayerLiveObject _player;
    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private int _layerPlayer=9;
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.layer == _layerPlayer)
        {
            _player = other.gameObject.GetComponent<PlayerLiveObject>();
            _player.TakeDamage(_damage);
        }
    }
}
