using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleTrigger : MonoBehaviour
{
    private AIController aiController;

    private void Start()
    {
        aiController = GetComponentInParent<AIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null && aiController != null)
            {
                aiController.TacklePlayer(playerRigidbody, other.gameObject);
            }
        }
    }
}
