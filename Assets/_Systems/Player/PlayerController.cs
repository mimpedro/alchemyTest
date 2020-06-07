using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    public float turnAngle = 45f;
    private Rigidbody body;

    private int turn;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Vector2 input = new Vector2 ()
        //Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        //Vector3 inputDirection = forward * Input.GetAxis("Vertical") + Quaternion.Euler(Vector3.up * 90) * forward * Input.GetAxis("Horizontal");
        //body.AddForce(speed * inputDirection * Time.deltaTime);
        if (Input.GetAxis("LookHorizontal") > 0.9f)
        {
            if (turn < 1)
            {
                Turn(1);
            }
            turn = 1;
        } else if (Input.GetAxis("LookHorizontal") < -0.9f)
        {
            if (turn > -1)
            {
                Turn(-1);
            }
            turn = -1;
        }
        else
        {
            turn = 0;
        }
    }

    void Turn(int direction)
    {
        transform.Rotate(transform.up * direction * turnAngle);
    }
}
