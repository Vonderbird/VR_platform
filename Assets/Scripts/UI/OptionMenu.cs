//////////////////////////////////////////////////////
// 
// desc:    Option Menu for object that is selected
//          will pops up on your vr controller
//
// author:  amirardroudi
//
//////////////////////////////////////////////////////

using System;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;



public class OptionMenu : MonoBehaviour
{
    [Header("VR References")]
    public SteamVR_Action_Boolean OpenOptionAction;
    public SteamVR_Action_Boolean scalePadActionLeft;
    public SteamVR_Action_Boolean scalePadActionRight;
    
    public Transform leftController;
    public Transform rightController;
    public Transform headset;
    public Pointer pointerRef;
    
    [Header("UI References")]
    public Toggle gravityToggle;          // Toggling gravity in object's rigidbody
    public Slider massSlider;             // changing mass in object's rigidbody
    public Button rigidbodybutton;        //for removing rigidbody
    public Button deleteButton;           // Delete gameObject
    private Hand rightHand;
    private Hand leftHand;
    private Transform SelectedObject;
    private Interactable selectedobjInteraction;
    private Quaternion orginalRotation;
    private float currentDisControllers;


    

    #region UnityEvents
    private void Awake() 
    {
        orginalRotation = transform.rotation;
        leftHand  = leftController.GetComponent<Hand>();
        rightHand = rightController.GetComponent<Hand>();
    }
    private void OnEnable()
    {
        if (OpenOptionAction == null)
        {
            return;
        }
        OpenOptionAction.AddOnChangeListener(ActivateOptionMenu,  rightHand.handType);
        
        gravityToggle.onValueChanged.AddListener(delegate { UseGravity(gravityToggle);});
        massSlider.onValueChanged.AddListener(delegate { MassModifier();});
        rigidbodybutton.onClick.AddListener(RemoveRigidbody);
    }

    private void OnDisable()
    {
        if(OpenOptionAction != null)
            OpenOptionAction.RemoveOnChangeListener(ActivateOptionMenu, rightHand.handType);
    }

    private void Update()
    {
        // set rotation of option menu related to camera
        UpdateRotation();
    }
    #endregion

    // ================== CALL WHEN CLICK ON MENU BUTTON CONTROLLER ==================   
    private void ActivateOptionMenu(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        GetComponent<Canvas>().enabled = newValue;
        
        //get reference of hitted object by raycast
        SelectedObject = pointerRef.GetHitedObject();
        
        selectedobjInteraction = SelectedObject.GetComponent<Interactable>();
        
        if (newValue)
        {
            SyncOptionToObject();
        }
    }
    // ================================================================================
    
    private void SyncOptionToObject()
    {
        // Bind Destroy function to delete button
        deleteButton.onClick.AddListener(DestroyObject);
        
        // sync object rigidbody values with UI
        var objRigid = SelectedObject.GetComponent<Rigidbody>();
        if (objRigid != null)
        {
            massSlider.value = objRigid.mass;
            gravityToggle.isOn = objRigid.useGravity;
        }
        
        //sync position of option menu on top of object
        SetOptionMenuPos();
    }
    
    // ====================== OPTION MENU POSITION ======================  
    private void SetOptionMenuPos()
    {
        transform.position = SelectedObject.position + Vector3.up * 1.5f;
    }

    // ====================== OPTION MENU ROTATION ======================  
    private void UpdateRotation()
    {
        transform.rotation = headset.rotation * orginalRotation;
    }

    
    // ====================== Gravity ======================  
    private void UseGravity(Toggle toggle)
    {
        if (selectedobjInteraction == null) return;
        
        selectedobjInteraction.Gravity(toggle.isOn);
    }
    
    // ====================== Mass ======================  
    private void MassModifier()
    {
        if (selectedobjInteraction != null)
        {
            selectedobjInteraction.ModifyMass(massSlider.value);
        }
    }
    
    // ====================== Remove Rigidbody ======================  
    private void RemoveRigidbody()
    {
        if(selectedobjInteraction.GetRigidbody())
            selectedobjInteraction.RemoveRigidbody();
    }
    
    
    // ====================== Destroy Object ======================  
    private void DestroyObject()
    {
        if(selectedobjInteraction != null)
            selectedobjInteraction.DestroyObject();
    }
    
    
        
    // ====================== Scale Object ======================  
    private void ScaleObject()
    {
        if (scalePadActionLeft.GetState(leftHand.handType) && scalePadActionRight.GetState(rightHand.handType))
        {
            Debug.Log("both trigger as scaling action pressed!");
        }
        // if both controller grab pressed
            // cash distance of controllers when grab on both controllers pressed
                // if cashedDistance < currentDisControllers
                    //scale object
    }
    
    private float UpdateDisControllers()
    {
        float distance = Vector3.SqrMagnitude(leftController.position - rightController.position);
        return distance;
    }
    
}
