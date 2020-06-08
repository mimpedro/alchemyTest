using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyWeapon : MonoBehaviour
{
    public float reach = 10f;
    public AnimationCurve gestureMultiplierByDistance;
    //public GameObject pillarPrefab;
    public Reticle reticle;
    public string activateInputAxis;
    public LayerMask buildableLayerMask;
    public LayerMask blockingLayerMask;

    private Vector3 initialHandPosition;
    //private Vector3 initialHeadPosition;
    private State state;
    private AlchemyPillar currentPillar;
    private Renderer reticleRenderer;
    //private List<AlchemyPillar> pillarsPool;

    public enum State
    {
        Ready,
        Creating
    }

    private void Awake()
    {
        reticleRenderer = reticle.GetComponent<Renderer>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Ready:
                RaycastHit hit;
                Vector3 direction = transform.forward;
                if (Vector3.Dot(direction, Vector3.down) > 0.8f) direction = Vector3.down;
                if (Physics.Raycast(transform.position, direction, out hit, reach, blockingLayerMask))
                {
                    reticle.gameObject.SetActive(true);
                    reticle.transform.position = hit.point;
                    reticle.transform.up = hit.normal;

                    if (buildableLayerMask == (buildableLayerMask | (1 << hit.collider.gameObject.layer)))
                    {
                        reticle.ChangeStatus(true);
                        
                        if (Input.GetAxis(activateInputAxis) > 0.5)
                        {
                            state = State.Creating;
                            CreatePillar(hit);

                            initialHandPosition = transform.position - XRRig.instance.transform.position;
                            //initialHeadPosition = Camera.main.transform.position;
                        }
                    }
                    else
                    {
                        reticle.ChangeStatus(false);
                    }
                }
                else
                {
                    reticle.gameObject.SetActive(false);
                }
                break;
            case State.Creating:
                reticle.gameObject.SetActive(false);
                currentPillar.SetHeight(GetGestureAmplitude());
                if (Input.GetAxis(activateInputAxis) < 0.5)
                {
                    state = State.Ready; 
                }
                break;
        }
    }

    public void CreatePillar(RaycastHit hit)
    {
        var ss = hit.collider.GetComponent<SuperStone>();
        if (ss != null) ss.SetReady(false);
        GameObject newPillarObj = AlchemyPillarPool.instance.CreatePillar(hit.point, hit.normal, hit.collider.tag);

        currentPillar = newPillarObj.GetComponent<AlchemyPillar>();
        currentPillar.SetHeight(0);
    }

    public float GetGestureAmplitude()
    {
        Vector3 gesture = (transform.position - XRRig.instance.transform.position) - initialHandPosition;
        Vector3 gestureOnNormal = Vector3.Project(gesture, currentPillar.transform.up);
        float sign = Mathf.Sign(Vector3.Dot(gesture, currentPillar.transform.up));
        float distanceMultiplier = currentPillar.maxHeight * gestureMultiplierByDistance.Evaluate(Vector3.Distance(initialHandPosition, currentPillar.transform.position));
        return gestureOnNormal.magnitude * sign * distanceMultiplier;
    }
}
