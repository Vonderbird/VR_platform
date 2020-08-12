using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Rigidbody rigidRef;
    private void OnEnable()
    {
        var rigid = GetComponent<Rigidbody>();
        if (rigid != null)
            rigidRef = rigid;
    }

    public void DestroyObject()
    {
        Destroy(this);
    }

    public void AddRigidBody(float gravityAmount)
    {
        rigidRef = gameObject.AddComponent<Rigidbody>();
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
