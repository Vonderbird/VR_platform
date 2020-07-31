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
    void Update()
    {
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }
        // Release the object with trigger
        if (SteamVR_Actions._default.GrabPinch.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            if (generatedObj != null)
            {
                generatedObj.parent = null;
                generatedObj = null;
            }
        }

        if (SteamVR_Actions._default.GrabGrip.GetLastState(SteamVR_Input_Sources.Any))
        {
            AttachObject();
        }

        ChangeRayLength();

    }
    
    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab, pointerRef.GetEndPosition() , rightController.transform.rotation, rightController.transform);
    }

    public void ChangeRayLength()
    {
        pointerRef.m_DefaultLength = (generatedObj == null) ? 12.0f : 5.0f;
    }

    public void AttachObject()
    {
        Transform obj = pointerRef.HitedObject();
        if (obj.GetComponent<ObjectScript>())
        {
            obj.GetComponent<ObjectScript>().attached = true;
            obj.parent = rightController.transform;
            Debug.Log(obj.name+ " attached to : "+obj.parent.name);
        }
    }
}
