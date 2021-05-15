using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLegs : MonoBehaviour
{
    public float rotateSpeed;

    void Start()
    {
        
    }
    void Update()
    {
        Rotate();
        
    }
    void Rotate()
    {
        transform.gameObject.transform.Rotate(rotateSpeed*Time.deltaTime,0,0);
    }
   
}


