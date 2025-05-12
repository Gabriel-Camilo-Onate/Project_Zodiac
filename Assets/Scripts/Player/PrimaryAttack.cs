using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryAttack : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private int _layerEnemy=8;
    [SerializeField]
    private int _layerTrapEnemy=14;
    private LiveObject _liveObject;
    [SerializeField]
    public int _damage;
    [SerializeField]
    private float _timeToDisapear;
    public bool _isBuff;
    [SerializeField] 
    GameObject _mist;
    void Start()
    {
        if (!_particleSystem)
            _particleSystem = GetComponent<ParticleSystem>();
        if (!_isBuff)
        {
            _mist.GetComponent<ParticleSystem>().startColor = Color.white;
        }
        else
        {
            _mist.GetComponent<ParticleSystem>().startColor = Color.yellow;
        }
        StartCoroutine(DisapearAfterTime());
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == _layerEnemy || other.gameObject.layer == _layerTrapEnemy)
        {
            _liveObject = other.GetComponent<LiveObject>();
            _liveObject.TakeDamage (_damage);
        }
        if(other.layer== 0)
        {
            Destroy(this.gameObject);

        }
    }
    private IEnumerator DisapearAfterTime()
    {
        yield return new WaitForSeconds(_timeToDisapear);
        Destroy(this.gameObject);
    }
}
