using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onContact : MonoBehaviour
{
    [SerializeField] bool damangeable;
    [SerializeField] bool activator;
    [SerializeField] GameObject _object;
    [SerializeField] bool animator;
    [SerializeField] string _boolAnimatorEnable;
    [SerializeField] bool teleporter;
    [SerializeField] Vector3 vectorPosition;
    [SerializeField] bool sceneChanger;
    [SerializeField] string _sceneChange;

    private void Start()
    {
        if (damangeable == false && activator == false && animator == false && teleporter == false && sceneChanger == false)
            Debug.LogError("onContact script does not have a boolean assigned in the object " + gameObject.name.ToString());
    }
    private void Update()
    {
        if (activator == true)
        {
            if (_object == null)
                Destroy(gameObject);
        }
        else
            return;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player" && animator == true)
        {
            _object.GetComponent<Animator>().SetBool(_boolAnimatorEnable, true);
        }
        if (other.gameObject.tag == "Player" && teleporter == true)
        {
            other.transform.position = vectorPosition;
        }
        if (other.gameObject.tag == "Player" && sceneChanger == true)
        {
            SceneManager.LoadScene(_sceneChange);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && damangeable == true)
        {
            var Player = other.GetComponent<PlayerLiveObject>();
            Player.TakeDamage(600);
        }
    }

}
