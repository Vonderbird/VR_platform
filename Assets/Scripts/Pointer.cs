using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 12.0f;
    public GameObject m_Dot;
    public InputModule m_InputModule;
    private LineRenderer m_LineRenderer = null;

    private Vector3 EndPosition;

    public RaycastHit hitedRay;
    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //use default value from our input module or distance
        PointerEventData data = m_InputModule.GetData(); 
        float TargetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;
        //Raycast
        hitedRay = createRaycast(TargetLength);
        //Default
        EndPosition = transform.position + (transform.forward * TargetLength);
        //Update when hitting something (Check for collider)
        if (hitedRay.collider != null)
        {
            EndPosition = hitedRay.point;
        }

        //Set position of the Dot
        m_Dot.transform.position = EndPosition;
        //Set position of LineRenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, EndPosition);
        

    }

    private RaycastHit createRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position,transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        
        return hit;
    }

    public Transform HitedObject()
    {
        if (hitedRay.transform != null)
        {
            Debug.Log(hitedRay.transform.name);
            return hitedRay.transform;
        }
        return null;
    }
    
    public Vector3 GetEndPosition()
    {
        return EndPosition;
    }
}
