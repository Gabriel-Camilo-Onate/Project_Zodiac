using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerManager : MonoBehaviour
{
    public GameObject child;
     private void OnTriggerEnter(Collider other)
    {
     //   child.GetComponent<TrapEnemy>().triggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
       // child.GetComponent<TrapEnemy>().triggerExit(other);
    }
}
