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
    public RaycastHit hitedRay;             // the ray which will check for collision object
    private Transform hitedObject;
    private LineRenderer m_LineRenderer = null;
    private Vector3 EndPosition;
    
    private void Awake()    
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

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
        hitedRay = CreateRaycast(TargetLength);
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

    private RaycastHit CreateRaycast(float length)
    {
        Ray ray = new Ray(transform.position,transform.forward);
        if(Physics.Raycast(ray, out hitedRay, m_DefaultLength))
        {
            hitedObject = hitedRay.transform;
        }
        return hitedRay;
    }

    
    public Transform GetHitedObject()
    {
        return hitedObject;
    }
    public Vector3 GetEndPosition()
    {
        return EndPosition;
    }
}
