using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerMovement : MonoBehaviour
{
    public float[] speeds;
    public Collider playerCollider;

    private Rigidbody body;
    private PlayerController playerController;
    private bool active = false;
    private int currentSpeed;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            DebugMode(!active);
        }
        if (active)
        {
            if (Input.GetButtonDown("Jump")){
                currentSpeed++;
                if (currentSpeed > speeds.Length - 1) currentSpeed = 0;
            }
            Vector3 movementDirection = Camera.main.transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            transform.position += Time.deltaTime * speeds[currentSpeed] * movementDirection;
        }
    }

    void DebugMode(bool active)
    {
        Debug.Log("Debug movement is " + active);
        playerController.enabled = !active;
        playerCollider.enabled = !active;
        body.isKinematic = active;
        this.active = active;
    }
}
