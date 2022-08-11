using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject hedef;
    public Vector3 mesafe;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position=Vector3.Lerp(transform.position,hedef.transform.position+mesafe,Time.deltaTime);
    }
}
