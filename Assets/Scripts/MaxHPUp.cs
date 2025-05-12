using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHPUp : objectCollisionable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private ItemManager _player;
    [SerializeField] private int _amountToPlusMaxLife;
    private const int _constZero = 0;
    [SerializeField] private float _timeToDestroyAfterCollision;
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] AudioClip _audioClip;
    //private string _itemType = "MaxHPPotion";

    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_sphereCollider == null)
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }
    }
    private void Update()
    {
        transform.Rotate(_rotationDirection, _rotationSpeed * Time.deltaTime);
    }
    public override void Action(GameObject player)
    {
        _player = player.GetComponent<ItemManager>();
        _sphereCollider.enabled = false;
        _mesh.enabled = false;
        _player.ItemCounter(3);
      
        StartCoroutine(SoundAndDestroy());
    }
    //public override void UseAction()
    //{
    //    _player.AddMaxLife(_amountToPlusMaxLife);
    //    Destroy(gameObject);
    //}
    public IEnumerator SoundAndDestroy()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        yield return new WaitForSeconds(_timeToDestroyAfterCollision);
        Destroy(gameObject);
    }
}
