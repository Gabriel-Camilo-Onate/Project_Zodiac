using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonBehavior : MonoBehaviour
{
    public GameObject fader;
    public string boolName;
    public string sceneToChange;
   
    public void OnMouseOver()
    {
        gameObject.GetComponent<Animator>().SetBool(boolName, true);
        Debug.Log("a");
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<Animator>().SetBool(boolName, false);
    }

    public void OnClick()
    {
        fader.GetComponent<Animator>().SetTrigger("ExitLevel");
        fader.GetComponent<FaderManager>().SetTarget(sceneToChange);
        fader.GetComponent<FaderManager>().SceneExit(); ;
    }

    public void ForExitButton()
    {
        Application.Quit();
    }
}
