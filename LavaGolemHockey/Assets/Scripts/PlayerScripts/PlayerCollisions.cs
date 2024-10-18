using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public bool hasPuck = false;
    public GameObject puckVisual;

    private Vector3 leftPlayerInitialPosition;
    private Vector3 rightPlayerInitialPosition;

    private Vector3 p2LeftPlayerInitialPosition;
    private Vector3 p2RightPlayerInitialPosition;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void Start()
    {
        if (this.CompareTag("p1L"))
        {
            leftPlayerInitialPosition = transform.position;
        }
        if (this.CompareTag("p1R"))
        {
            rightPlayerInitialPosition = transform.position;
        }
        if (this.CompareTag("p2L"))
        {
            p2LeftPlayerInitialPosition = transform.position;
        }
        if (this.CompareTag("p2R"))
        {
            p2RightPlayerInitialPosition = transform.position;
        }
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
            puckVisual.SetActive(false);
            ResetPosition();
        }
    }

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

    public void ResetPosition()
    {
        /*this.transform.position = Vector3.zero;*/

        if (this.CompareTag("p1L"))
        {
            transform.position = leftPlayerInitialPosition;
        }
        if (this.CompareTag("p1R"))
        {
            transform.position = rightPlayerInitialPosition;
        }
        if (this.CompareTag("p2L"))
        {
            transform.position = p2LeftPlayerInitialPosition;
        }
        if (this.CompareTag("p2R"))
        {
            transform.position = p2RightPlayerInitialPosition;
        }
    }

}
