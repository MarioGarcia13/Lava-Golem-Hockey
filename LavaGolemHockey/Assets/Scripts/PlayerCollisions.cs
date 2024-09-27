using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public bool hasPuck = false;
    public GameObject puckVisual;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            hasPuck = true;
            puckVisual.SetActive(true);
            Destroy(collision.gameObject);
            transform.parent.GetComponent<PlayerController>().CollisionDetected(this);
        }
    }

    public void RemovePuck()
    {
        if (hasPuck)
        {
            Debug.Log("removing puck");
            hasPuck = false;
            puckVisual.SetActive(false);
        }
    }
}
