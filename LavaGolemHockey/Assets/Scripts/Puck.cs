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

    void Update()
    {
        
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Push Puck"))
        {
            rb.AddForce(new Vector3(force, 0f, 0f), ForceMode.Impulse);
        }
    }
}
