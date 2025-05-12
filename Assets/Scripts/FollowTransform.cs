using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    private Transform _transformToFollow;
    void Update()
    {
        if (_transformToFollow != null)
        {
        transform.position = _transformToFollow.position;   
        }
        else
        {
            DestroyParticle();
        }
    }
    public void SetTransformToFollow(Transform transformToFollow)
    {
        _transformToFollow = transformToFollow;
    }
    public void DestroyParticle()
    {
        Destroy(gameObject);
    }
}
