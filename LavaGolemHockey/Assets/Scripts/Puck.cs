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
        rb.AddForce(new Vector3(force, 0f, 0f), ForceMode.Impulse);
    }


    void Update()
    {
        
    }
}
