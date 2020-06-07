using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStone : MonoBehaviour
{
    public float respawnDuration = 60;
    public LayerMask depletedLayer;
    public Material depletedMaterial;
    public Material readyMaterial;
    
    private float timer;
    private MeshRenderer rend;
    private bool ready = true;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (!ready)
        {
            timer += Time.deltaTime;
            if (timer > respawnDuration) SetReady(true);
        }
    }
    public void SetReady(bool ready)
    {
        rend.material = ready ? readyMaterial : depletedMaterial;
        gameObject.layer = LayerMask.NameToLayer(ready ? "Terrain" : "Default");

        if (!ready)
        {
            timer = 0;
        }
    }
}
