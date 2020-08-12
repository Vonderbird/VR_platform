using UnityEngine;
using Valve.VR;

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
    
    private void Awake()
    {
        objectAttached = false;
    }

    private void OnEnable()
    {
        if (grabAction == null)
        {
            Debug.LogError("<b>[StreamVR Interaction]</b> Grab action assigned!");
            return;
        }
        grabAction.AddOnChangeListener(GrabReleaseObject, SteamVR_Input_Sources.Any);
    }
    private void OnDisable()
    {
        if(grabAction != null)
            grabAction.RemoveOnChangeListener(GrabReleaseObject ,SteamVR_Input_Sources.Any);
    }


    // TODO: change input system to events (Remove them from update method)
    void Update()
    {
        // show tool menu
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }
        
        // Release the object with trigger
        // SteamVR_ActionsGrabReleaseObject(SteamVR_Actions._default.GrabGrip.GetLastState(SteamVR_Input_Sources.Any));
    }
    



    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition() - Vector3.back*1.5f,
            rightController.transform.rotation);
    }

    private void GrabReleaseObject(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources  inputSources, bool newValue)
    {
        hittedObj = pointerRef.transform;
        if (newValue)
        {
            if (hittedObj.CompareTag(interactableObjTag))
            {
                hittedObj.parent = rightController.transform;
                objectAttached = true;
            }
        }
        // if grab released
        else
        {
            hittedObj.parent = null;
            objectAttached = false;
        }




        /*
        // if no object attached to controller
        if(pointerRef.GetHitedObject() && pointerRef.GetHitedObject().CompareTag(interactableObjectTag))
        {
            if (triggerState && objectAttached == false)
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
        */
    }
    
}
