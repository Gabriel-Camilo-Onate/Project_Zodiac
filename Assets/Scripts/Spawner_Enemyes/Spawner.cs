using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private LiveObject _thisLiveObject;

    [SerializeField]
    private LiveObject _liveObject;
    [SerializeField]
    private float _spawnInterval;
    [SerializeField]
    private List<LiveObject> _spawnedEnemies;
    [SerializeField]
    private int _maxEnemiesAtOnce=3;
    private LiveObject _spawnedEnemyInstance;
    [SerializeField]
    private int _timeForDisapear;
    [SerializeField]
    private Vector3 _offsetSpawn;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private Vector3 _explosionScale;


    void Start()
    {
        if (_thisLiveObject == null)
            _thisLiveObject = GetComponent<LiveObject>();
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (_thisLiveObject._isDead)
        {
            StopCoroutine(Spawn());
            StartCoroutine(Death());
        }
    }
        private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            if (_spawnedEnemies.Count < _maxEnemiesAtOnce)
            {
                _spawnedEnemyInstance = Instantiate<LiveObject>(_liveObject, transform.position + _offsetSpawn, _liveObject.transform.rotation);
                _spawnedEnemyInstance.GetComponent<EntitysActivator>()._isActive = true;
                _spawnedEnemies.Add(_spawnedEnemyInstance);
                _spawnedEnemyInstance._onDeath += OnSpawnedEnemyDied;
            }

        }
    }
    private void OnSpawnedEnemyDied(LiveObject _enemyThatDied)
        {
            _spawnedEnemies.Remove(_enemyThatDied);
        }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(_timeForDisapear);
        if (_explosion != null)
        {
            GameObject explosion= Instantiate(_explosion, transform.position, transform.rotation);
            explosion.transform.localScale = _explosionScale;
        }
        Destroy(gameObject);
    }

    }
