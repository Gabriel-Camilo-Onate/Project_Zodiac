using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMissile : MonoBehaviour
{
    [SerializeField]
    int _damage;
    [SerializeField]
    int _speed;
    [SerializeField]
    int _playerLayer = 9;
    Rigidbody _rb;
    public Vector3 direcc;
    private PlayerLiveObject _liveObject;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Timer());
    }

    void Update()
    {
        Fly(direcc);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _playerLayer)
        {
            _liveObject = collision.gameObject.GetComponent<PlayerLiveObject>();
            _liveObject.TakeDamage(_damage);
            Destroy(gameObject);
        } 
    }

    private void Fly(Vector3 v)
    {
        _rb.velocity = v * _speed * Time.deltaTime;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
