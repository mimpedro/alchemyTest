using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
    }

    public void ChangeStatus(bool ready)
    {
        rend.material.color = ready ? Color.green : Color.red;
    }
}
