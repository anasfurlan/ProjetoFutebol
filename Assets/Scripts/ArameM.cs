using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArameM : MonoBehaviour
{
   private SliderJoint2D arame; 
   private JointMotor2D aux;


    void Start()
    {
        arame = GetComponent <SliderJoint2D> ();
        aux = arame.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if(arame.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range (-1, -5);
            arame.motor = aux;
        }
        
         if(arame.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range (1, 5);
            arame.motor = aux;
        }
    }
}
