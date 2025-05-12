using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCollisionable : MonoBehaviour
{
    [SerializeField]
    protected int _layerPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if(_layerPlayer==other.gameObject.layer)
        {
            Action(other.gameObject);
        }
    }

    public virtual void Action(GameObject player) { }
    public virtual void UseAction() { }
    protected IEnumerator DestroyAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
