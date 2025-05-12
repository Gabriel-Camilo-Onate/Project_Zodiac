using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalObject : objectCollisionable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _cristalSound;
    [SerializeField] private BoxCollider _boxCollider;
    private Transform[] _children;
    private const int _constZero = 0;
    private const int _constOne = 1;
    [SerializeField] private float _timeToDestroyAfterCollision;
    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_boxCollider == null)
        {
            _boxCollider = GetComponent<BoxCollider>();
        }
        _children = GetComponentsInChildren<Transform>();
        _audioSource.clip = _cristalSound;
    }
    public override void Action(GameObject player)
    {
        _audioSource.Play();
        GameManager.instance.currentCristals++;
        GameManager.instance._txtObj.text =
        GameManager.instance.currentCristals.ToString() + "/" +
        GameManager.instance.cristalsToWin.ToString();
        GameManager.instance.Checker();
        _boxCollider.enabled = false;
        for (int i = _constZero; i < _children.Length - _constOne; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        StartCoroutine(DestroyAfterSeconds(_timeToDestroyAfterCollision));
    }
}
