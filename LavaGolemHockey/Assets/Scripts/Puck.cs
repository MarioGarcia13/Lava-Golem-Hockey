using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [Range(0f, 200f)]
    public float force;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    IEnumerator delayTimer()
    {
        //increase score depending on which goal was scored on
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Goal1")
        {
            //score1++
            StartCoroutine(delayTimer());
        }

        if (other.gameObject.tag == "Goal2")
        {
            //score2++
            StartCoroutine(delayTimer());
        }
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Forward"))
        {
            rb.AddForce(new Vector3(force, 0f, 0f), ForceMode.Impulse);
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "Up"))
        {
            rb.AddForce(new Vector3(0f, 0f, force), ForceMode.Impulse);
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "Down"))
        {
            rb.AddForce(new Vector3(0f, 0f, -force), ForceMode.Impulse);
        }
        if (GUI.Button(new Rect(10, 130, 100, 30), "Backwards"))
        {
            rb.AddForce(new Vector3(-force, 0f, 0f), ForceMode.Impulse);
        }
    }
}
