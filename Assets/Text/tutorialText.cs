using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialText : MonoBehaviour
{
    public int State;
    public Sprite[] imagenes;
    public GameObject finalText;
    public GameObject final2Text;

    void Start()
    {
        State = 1;
    }

    
    void Update()
    {
        switch (State)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = imagenes[0];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = imagenes[1];
                break;
            case 3:
                gameObject.GetComponent<Image>().sprite = imagenes[2];
                break;
            case 4:
                gameObject.GetComponent<Image>().enabled = false;
               finalText.SetActive(true);
                break;
            case 5:
                finalText.SetActive(false);
                final2Text.SetActive(true);
                break;
            case 6:
                final2Text.SetActive(false);
                gameObject.GetComponent<Image>().enabled = true;
                gameObject.GetComponent<Image>().sprite = imagenes[3];
                break;

        }

        if (State == 1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                State = 2;
            }
        }

        if (State == 2)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                State = 3;
            }
        }
        if (State == 3)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                State = 4;
            }
        }
        if (State == 4)
        {
            float x= Input.GetAxis("Horizontal");
            float z= Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.E) && (x!=0 || z!=0))
            {
                State = 5;
            }
        }
        if (State == 5)
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                State = 6;
            }
        }
        if (State == 6)
        {
            Destroy(gameObject, 3f);
        }
    }
}
