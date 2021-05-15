using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    float speed;
    public static bool changeSpeed;
   
    void Start()
    {
        changeSpeed = true;
    }

    void Update()
    {
        ConstantForce();
    }

    void ConstantForce()
    {
        if(changeSpeed)
        {
            speed = (3.3f*DrawLine.currentIndex);
        }

        transform.GetComponent<Rigidbody>().AddForce(0,0,speed);
    }
}
