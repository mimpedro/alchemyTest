using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyPillar : MonoBehaviour
{
    public float maxHeight;
    public float maxRisingSpeed;
    public float lifeTime = 10f;
    public bool playerRiding
    {
        get;
        private set;
    }

    private float timer;
    private float height;

    private void OnEnable()
    {
        playerRiding = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime) Destroy(gameObject);

        Debug.Log("PlayerRiding = " + playerRiding);
    }

    public void SetHeight(float newHeight)
    {
        if (playerRiding)
        {
            if (Mathf.Abs(height - newHeight) > 0.05f)
            {
                height += maxRisingSpeed * Time.deltaTime * Mathf.Sign(newHeight - height);
            }
        }
        else
        {
            height = newHeight; 
        }
        height = Mathf.Clamp(height, 0.01f, maxHeight);
        transform.localScale = Vector3.one + Vector3.up * (height - 1);
        timer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.5f){
            playerRiding = true;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")){
    //        //playerRiding = false;
    //    }
    //}
}
