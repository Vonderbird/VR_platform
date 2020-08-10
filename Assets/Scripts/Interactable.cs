using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool selected;
    private Rigidbody rigidRef;
    private void OnEnable()
    {
        if (GetComponent<Rigidbody>())
            rigidRef = GetComponent<Rigidbody>();
    }

    public void DestroyObject()
    {
        Destroy(this);
    }

    public void AddRigidBody(float gravityAmount)
    {
        //if gravity slider was more than 0
        
    }

    public void RemoveRigidBody()
    {
        //if gravity slider in option menu was zero
            //setRigidbody  to kinematic
            
    }
    public void ModifyGravity(float gravityAmount)
    {
        if (gravityAmount <= 0)
        {
            rigidRef.isKinematic = true;
        }
        else
        {
            rigidRef.mass = gravityAmount;
        }
    }
    
    
    
    
    
}
