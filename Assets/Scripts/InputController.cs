using UnityEngine;
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
    
    [Header("SteamVR References")]
    public SteamVR_Action_Boolean grabAction;
    public Hand hand;
    
    private Transform hittedObj;
    private Interactable HittedObjInteractable;
    
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
        // grab object
        grabAction.AddOnStateDownListener(GrabObject, hand.handType);
        // release object
        grabAction.AddOnStateUpListener(ReleaseObject, hand.handType);
    }
    private void OnDisable()
    {
        if (grabAction != null)
        {
            grabAction.RemoveOnStateDownListener(GrabObject , hand.handType);
            grabAction.RemoveOnStateUpListener(ReleaseObject , hand.handType);
        }
    }


    // TODO: change input system to events (Remove them from update method)
    void Update()
    {
        // show tool menu
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            menuManagerRef.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }
    }

    public void InstantiateObject(Transform objectPrefab)
    {
        generatedObj = Instantiate(objectPrefab,
            pointerRef.GetEndPosition() - Vector3.up*1.3f,
            rightController.transform.rotation);
    }

    private void ReleaseObject(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources  inputSources)
    {
        var rigid = HittedObjInteractable.GetRigidbody();

        //set gravity to previous state before grabbing
        if (rigid)
            rigid.useGravity = HittedObjInteractable.cachedRigid.gravityState;
        
        hittedObj.parent = null;
        objectAttached = false;
        Debug.Log(hittedObj.name + " released from controller!");
        hittedObj = null;
    }
    
    /// <summary>
    /// Grab an object on pressing grabAction
    /// </summary>
    private void GrabObject(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources  inputSources)
    {
            hittedObj = pointerRef.GetHitedObject();
            if(!hittedObj) return;
            
            HittedObjInteractable = hittedObj.GetComponent<Interactable>();
            
            if (!hittedObj.CompareTag(interactableObjTag) && !objectAttached) return;
            Debug.Log(hittedObj.name + " Grabbed!");
            
            //disable gravity when grabbing object
            if (HittedObjInteractable.GetRigidbody())
                HittedObjInteractable.GetRigidbody().useGravity = false;
            
            hittedObj.parent = rightController.transform;
            objectAttached = true;
        
    }
    
    
}
