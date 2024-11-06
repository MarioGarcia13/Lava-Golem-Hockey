using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public bool isTackling = false;
    public bool hasPuck = false;
    public GameObject puckVisual;
    public GameObject puckPrefab;

    [Range(0f, 3f)]
    public float stunTime = 1.5f;

    private Vector3 initialPosition;
    private PlayerController playerController;
    private Rigidbody rb;
    public bool isStunned = false;

    public BoxCollider tackleCollider;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
        playerController = transform.parent.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    public void HandleGameStateChanged(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.NewRound)
        {
            ResetPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCollisions otherPlayer = other.gameObject.GetComponent<PlayerCollisions>();
        if (otherPlayer != null && isTackling && !isStunned)
        {
            if (otherPlayer.hasPuck)
            {
                TacklePlayer(otherPlayer);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puck") && !isStunned)
        {
            PickUpPuck(collision.gameObject);
        }
    }

    private void PickUpPuck(GameObject puck)
    {
        hasPuck = true;
        puckVisual.SetActive(true);
        Destroy(puck);
        playerController.UpdatePuckStatus(this);
    }

    private void TacklePlayer(PlayerCollisions tackledPlayer)
    {
        if (tackledPlayer.hasPuck)
        {
            tackledPlayer.DropPuck();
            StartCoroutine(StunPlayer(tackledPlayer));
        }
    }

    public void DropPuck()
    {
        if (hasPuck)
        {
            hasPuck = false;
            puckVisual.SetActive(false);
            Instantiate(puckPrefab, transform.position, Quaternion.identity);
            playerController.UpdatePuckStatus(this);
        }
    }

    private IEnumerator StunPlayer(PlayerCollisions player)
    {
        player.isStunned = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(stunTime);

        player.isStunned = false;
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        hasPuck = false;
        puckVisual.SetActive(false);
        isStunned = false;
        rb.isKinematic = false;
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
