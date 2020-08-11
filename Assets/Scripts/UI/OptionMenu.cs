//////////////////////////////////////////////////////
// desc:    Option Menu for object that is selected
//          will pops up on your vr controller
//
// author:  amirardroudi
//////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OptionMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean OpenOptionAction;
    public Transform SelectedObject;
    public bool OptionMenuIsVisible = false;

    public Transform headset;
    public Pointer pointerRef;
    public Button deleteButton;
    public Slider gravitySlider;
    public Hand hand;
    private void OnEnable()
    {
        if (OpenOptionAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> No open OptionMenu action assigned!");
            return;
        }
        OpenOptionAction.AddOnChangeListener(ActiveOptionMenu, SteamVR_Input_Sources.RightHand);
    }

    private void OnDisable()
    {
        if(OpenOptionAction != null)
            OpenOptionAction.RemoveOnChangeListener(ActiveOptionMenu,SteamVR_Input_Sources.RightHand);
    }

    private void Update()
    {
        SelectedObject = pointerRef.GetHitedObject();
        if (SteamVR_Actions._default.OptionMenu.stateDown)
        {
            Debug.Log("option menu button clicked");
        }
    }

    private void ActiveOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        GetComponent<Canvas>().enabled = newValue;
        
        if (newValue)
        {
            SyncOptionToObject();
        }

        Debug.Log("Option button clicked: " + newValue);
    }
    
    private void SyncOptionToObject()
    {
        // Bind Destroy function to delete button
        deleteButton.onClick.AddListener(DestroyObject);
        
        // set slider value from mass of selected object rigidbody.mass
        if (SelectedObject.GetComponent<Rigidbody>() != null)
        {
            gravitySlider.value = SelectedObject.GetComponent<Rigidbody>().mass;    
        }
    }
    
    // set position and rotation of option menu related to VR headset and selected object
    private void SetOptionMenuPos()
    {
        //SelectedObject.position()
            
    }

    private void UpdateRotation()
    {
        transform.LookAt(headset);
    }
    private void DestroyObject()
    {
        SelectedObject.GetComponent<Interactable>().DestroyObject();
    }

    
    
}
