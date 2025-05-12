using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CristalCount : MonoBehaviour
{
    [SerializeField]
    private int _layerCristal;
    [SerializeField]
    private int _cristalsToWin;
    [SerializeField]
    private int _currentCristals;
    [SerializeField]
    private int _victoyyScene;
    private bool _checker;
    [SerializeField]
    private AudioClip _cristalSound;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private string _text = "/5";
    [SerializeField]
    private Text _txtObj;
    [SerializeField]
    private int _nextScene;
    private void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerCristal)
        {
            if (_checker)
                return;
            Destroy(other.gameObject);
            _currentCristals+=1;
            _txtObj.text = _currentCristals + _text+_cristalsToWin;
            _checker = true;
            _audioSource.clip = _cristalSound;
            _audioSource.Play();
            StartCoroutine(WaitToCheck()); 

            if(_currentCristals >= _cristalsToWin)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(_nextScene);
            }
        }
    }
    private IEnumerator WaitToCheck()
    {
        yield return new WaitForSeconds(1);
        _checker = false;
    }

}
