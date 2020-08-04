﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputController : MonoBehaviour
{
    [Header("References")]
    public MenuManager menuManagerRef;
    public GameObject rightController;
    public Pointer pointerRef;

    public Transform generatedObj;
    public bool objectAttached = false;
    public string interactableObjectTag;

    void Update()
    {
        // show tool menu
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }

        // Release the object with trigger
        GrabReleaseObject(SteamVR_Actions._default.GrabGrip.GetLastState(SteamVR_Input_Sources.Any));
    }
    



    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition(),
            rightController.transform.rotation);

        Debug.Log("instantiated object : " + generatedObj.name + " parent: " + generatedObj.parent);
    }

    public void GrabReleaseObject(bool triggerState)
    {
        // if no object attached to controller
        if(pointerRef.GetHitedObject() && pointerRef.GetHitedObject().CompareTag(interactableObjectTag))
        {
            if (triggerState)
            {
                pointerRef.GetHitedObject().parent = rightController.transform;
                objectAttached = true;
            }
            else
            {
                pointerRef.GetHitedObject().parent = null;
                objectAttached = false;
            }
        }
    }
    
}
