﻿using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputController : MonoBehaviour
{
    [Header("References")]
    public MenuManager menuManagerRef;
    public GameObject rightController;
    public Pointer pointerRef;

    public Transform generatedObj;
    public bool objectAttached;
    public string interactableObjTag;
    
    public SteamVR_Action_Boolean grabAction;
    private Transform hittedObj;
    public Hand hand;
    
    
    private void Awake()
    {
        objectAttached = false;
    }

    private void OnEnable()
    {
        if (grabAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> Grab action assigned!", this);
            return;
        }
        grabAction.AddOnChangeListener(GrabReleaseObject, hand.handType);
    }
    private void OnDisable()
    {
        if(grabAction != null)
            grabAction.RemoveOnChangeListener(GrabReleaseObject , hand.handType);
    }


    // TODO: change input system to events (Remove them from update method)
    void Update()
    {
        // show tool menu
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }

        hittedObj = pointerRef.GetHitedObject();
    }

    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition() - Vector3.back*1.5f,
            rightController.transform.rotation);
    }

    private void GrabReleaseObject(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources  inputSources, bool newValue)
    {
        
        if (newValue)
        {
            Debug.Log( hittedObj + " - Object is about to be grabbed!" );
            if (!hittedObj.CompareTag(interactableObjTag) && !objectAttached ) return;
            hittedObj.parent = rightController.transform;
            objectAttached = true;
        }
        // if grab released
        else
        {
            hittedObj.parent = null;
            objectAttached = false;
        }

    }
    
}
