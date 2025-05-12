using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentCristals;
    public int cristalsToWin;
    public Text _txtObj;
    [SerializeField]
    private PlayerFunctions _playerFunctions;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(_playerFunctions==null)
        {
            _playerFunctions = FindObjectOfType<PlayerFunctions>();
            if (_playerFunctions == null)
            {
                Debug.LogError("No se encontró al objeto PlayerFunciionts");
                return;
            }
        }
    }
    public void Checker()
    {
        if(currentCristals >= cristalsToWin)
        {
            _playerFunctions.SetCanInvokeThePortal(true);
        }
    }
}
