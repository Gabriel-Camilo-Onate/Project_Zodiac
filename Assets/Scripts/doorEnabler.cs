using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorEnabler : MonoBehaviour
{
    [SerializeField] doorBehaviour door;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && door!=null)
        {
            door.Close();
        }
    }
}
