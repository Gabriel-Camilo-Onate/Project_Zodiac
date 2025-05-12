using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaderManager : MonoBehaviour
{
    [SerializeField]
    private string _target;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _animatorTrigger;
    [SerializeField]
    private string _animatorTriggerEnterLevel;
    private const int _constOne = 1;
    private void Start()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("No se encontró el componente Animator");
                return;
            }
        }
        _animator.SetTrigger(_animatorTriggerEnterLevel);
    }
    public void SceneExit()
    {
        _animator.SetTrigger(_animatorTrigger);
    }
    public void SceneChange()
    {
        if (_target == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ _constOne);
        }
        SceneManager.LoadScene(_target);
    }
    public void SetTarget(string target)
    {
        _target = target;
    }
}
