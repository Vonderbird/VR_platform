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
        Destroy(gameObject);
    }

    /// <summary>
    /// Enable gravity, it will add rigidbody component if there is no component. 
    /// </summary>
    /// <param name="state">get state of toggle and set it to state of gravity</param>
    public void Gravity(bool state)
    {
        if (!rigidRef)
            rigidRef = gameObject.AddComponent<Rigidbody>();

        rigidRef.useGravity = state;
    }
    
    public void ModifyMass(float massAmount)
    {
        if (!rigidRef)
            rigidRef = gameObject.AddComponent<Rigidbody>();
        
        if (massAmount <= 0)
        {
            rigidRef.mass = 0;
        }
        else
        {
            rigidRef.mass = massAmount;
        }
    }
    
    
    
    
    
}
