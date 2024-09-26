using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puck : MonoBehaviour
{
    //ScoreBoard Variables
    public static bool goal1Scored = false;
    public static bool goal2Scored = false;
    public static bool nextRound = false;

    [Range(0f, 200f)]
    public float force;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator delayTimer()
    {
        
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        nextRound = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Goal1")
        {
            goal1Scored = true;
            StartCoroutine(delayTimer());
        }

        if (other.gameObject.tag == "Goal2")
        {
            goal1Scored = true;
            StartCoroutine(delayTimer());
        }
    }

    public void shootPuck()
    {
        rb.AddForce(new Vector3(force, 0f, 0f), ForceMode.Impulse);
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
