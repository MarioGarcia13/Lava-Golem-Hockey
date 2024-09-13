using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    [Range(0f, 200f)]
    public float speed;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(5, 0, 0), ForceMode.Impulse);
    }


    void Update()
    {
        
    }
}
