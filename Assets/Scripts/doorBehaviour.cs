using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBehaviour : MonoBehaviour
{
    [SerializeField] GameObject cristal;
    [SerializeField]
    private bool _destroyDoorEnablerWhenUse;
    [SerializeField]
    private doorEnabler _doorEnabler;
    private void Start()
    {
        gameObject.SetActive(false);
        if (_destroyDoorEnablerWhenUse)
        {
        if (_doorEnabler == null)
        {
            _doorEnabler=FindObjectOfType<doorEnabler>();
            if (_doorEnabler == null)
            {
                Debug.LogError("No se pudo encontrar el objeto DoorEnabler");
                return;
            }
        }
        }
    }
    void Update()
    {
        if(cristal == null)
        {
            if (_destroyDoorEnablerWhenUse)
            {
                if (_doorEnabler != null)
            {
                Destroy(_doorEnabler);
            }
            }
            Destroy(gameObject);
        }
    }
    public void Close()
    {
        gameObject.SetActive(true);
    }
}
