using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct cachedRigidbody
{
    public float mass;
    public bool gravityState;
}
public class Interactable : MonoBehaviour
{
    private Rigidbody rigidRef;
    public cachedRigidbody cachedRigid;

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
        cachedRigid.gravityState = rigidRef.useGravity;

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
        
        cachedRigid.mass = rigidRef.mass;
    }

    public void RemoveRigidbody()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
    }
    public bool GetGravityState()
    {
        return rigidRef.useGravity;
    }

    public float GetMass()
    {
        return rigidRef.mass;
    }

    public Rigidbody GetRigidbody()
    {
        return rigidRef;
    }

    public void ModifyScale(float sliderValue)
    {
        float scaleValue = Mathf.Lerp(0.1f, 10, sliderValue);
        transform.localScale =new Vector3(scaleValue, scaleValue, scaleValue);
    }




}
