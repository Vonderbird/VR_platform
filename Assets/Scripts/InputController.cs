using System.Collections;
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

    void Update()
    {
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }
        // Release the object with trigger
        bool triggerState = SteamVR_Actions._default.GrabPinch.GetLastState(SteamVR_Input_Sources.Any);
        GrabReleaseObject(triggerState);
    }
    
    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition() - (pointerRef.transform.forward * 6.0f),
            rightController.transform.rotation,
            rightController.transform);
        
    }


    public void GrabReleaseObject(bool triggerState)
    {
        // if no object attached to controller
        Transform obj = pointerRef.HitedObject();
        // if ray hits not movable object -> return from function
        
        if (triggerState && !objectAttached)
        {
                // obj.GetComponent<ObjectScript>().attached = true;
                obj.parent = rightController.transform;
                objectAttached = true;
        }
        else if(!triggerState && objectAttached)
        {
                // obj.GetComponent<ObjectScript>().attached = false;
                obj.parent = null;
                objectAttached = false;
        }
    }
    
}
