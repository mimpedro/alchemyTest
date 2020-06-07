using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform shadow;
    public float skinWidth = 0.5f;
    public float floorOffset = 0.01f;

    private RaycastHit hit;

    void LateUpdate()
    {
        if (Physics.Raycast(transform.position + Vector3.up * skinWidth, Vector3.down, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            shadow.position = hit.point + Vector3.up * floorOffset;
        }
        else
        {
            shadow.position = transform.position + Vector3.down * 100;
        }
        
    }
}
