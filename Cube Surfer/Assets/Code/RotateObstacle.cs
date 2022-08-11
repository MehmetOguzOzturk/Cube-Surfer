using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObstacle : MonoBehaviour
{
   
    public float speed;
    void Update()
    {
        transform.Rotate(new Vector3(0,360,0)*Time.deltaTime*speed);
    }
}
