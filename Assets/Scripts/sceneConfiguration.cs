using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneConfiguration : MonoBehaviour
{
    [SerializeField] int _cristalsToWin;
    [SerializeField] string _sceneToChange;
    [SerializeField] UnityEngine.UI.Text _text;
    void Awake()
    {
        GameManager.instance.currentCristals = 0;
        GameManager.instance.cristalsToWin = _cristalsToWin;
      //  GameManager.instance.sceneToChange = _sceneToChange;
        GameManager.instance._txtObj = _text;
    }

}
