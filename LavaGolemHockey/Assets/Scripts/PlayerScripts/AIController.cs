using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIController : MonoBehaviour
{
    //AI Variables
    private bool isShootingInProgress = false;
    public Transform shootLocation;
    public Transform netLocation;
    public Transform[] targetLocations = new Transform[3];
    private int currentTargetIndex = 0;
    private GameObject puckToFollow;
    private float checkInterval = 1f;
    private float checkTimer = 0f;

    //Input Variables
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction moveLeft;
    private InputAction moveRight;

    //Player Variables
    private PlayerCollisions playerCollisions;
    public GameObject leftPlayer;
    public Transform leftPuckPos;
    public Rigidbody leftRB;
    public GameObject rightPlayer;
    public Transform rightPuckPos;
    public Rigidbody rightRB;

    [Range(0f, 3f)]
    public float tackleResetTime = 0.5f;

    public GameObject puckPrefab;
    [SerializeField]
    private bool leftPlayerHasPuck = false;
    [SerializeField]
    private bool rightPlayerHasPuck = false;

    [Range(1f, 200f)]
    public float maxSpeed = 10f;
    [Range(1f, 8000f)]
    public float moveForce = 4000f;
    [Range(1f, 100f)]
    public float rotationSpeed = 10f;

    float lungeForce = 20f;

    private Vector2 movementInputLeft;
    private Vector2 movementInputRight;

    //private Coroutine passCoroutine;
    private bool canControl = false;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void Start()
    {
        targetLocations[0] = GameObject.Find("1Shot").transform;
        targetLocations[1] = GameObject.Find("2Shot").transform;
        targetLocations[2] = GameObject.Find("3Shot").transform;
        netLocation = GameObject.Find("netLocation").transform;
        shootLocation = GameObject.Find("shootingLocation").transform;

        leftRB = leftPlayer.GetComponent<Rigidbody>();
        rightRB = rightPlayer.GetComponent<Rigidbody>();
        FindPuckToFollow();

    }

    private void OnDestroy()
    {
        // Unsubscribe from GameStateManager events
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    public void ResetPlayerPositions()
    {
        // Reset positions
        /*leftRB.position = leftPlayerInitialPosition;
        rightRB.position = rightPlayerInitialPosition;*/


        // Reset velocities
        leftRB.velocity = Vector3.zero;
        rightRB.velocity = Vector3.zero;

        // Reset rotations
        leftRB.rotation = Quaternion.identity;
        rightRB.rotation = Quaternion.identity;

        leftPlayerHasPuck = false;
        rightPlayerHasPuck = false;
    }

    public void HandleGameStateChanged(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.NewRound)
        {
            ResetPlayerPositions();
        }
        canControl = (newState == GameStateManager.GameState.Ready);
    }

    private void FixedUpdate()
    {
        if (canControl)
        {
            checkTimer += Time.fixedDeltaTime;
            if (checkTimer >= checkInterval)
            {
                FindPuckToFollow();
                checkTimer = 0f;
            }
            if (puckToFollow != null)
            {
                MoveTowardsPuck(leftRB);
                MoveTowardsPuck(rightRB);
            }
        }
    }

    private void ChooseNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targetLocations.Length;
    }

    private void FindPuckToFollow()
    {
        puckToFollow = GameObject.FindGameObjectWithTag("Puck");
    }

    public void UpdatePuckStatus(PlayerCollisions player)
    {
        if (player.gameObject == leftPlayer)
        {
            leftPlayerHasPuck = player.hasPuck;
            if (leftPlayerHasPuck)
            {
                ChooseNextTarget();
            }
        }
        else if (player.gameObject == rightPlayer)
        {
            rightPlayerHasPuck = player.hasPuck;
            if (rightPlayerHasPuck)
            {
                ChooseNextTarget();
            }
        }
    }

    private void MoveTowardsPuck(Rigidbody playerRigidbody)
    {
        PlayerCollisions playerCollisions = playerRigidbody.GetComponent<PlayerCollisions>();
        if (playerCollisions.isStunned || isShootingInProgress) return;

        Vector3 targetPosition;
        bool thisPlayerHasPuck = (playerRigidbody == leftRB && leftPlayerHasPuck) || (playerRigidbody == rightRB && rightPlayerHasPuck);
        bool otherPlayerHasPuck = (playerRigidbody == leftRB && rightPlayerHasPuck) || (playerRigidbody == rightRB && leftPlayerHasPuck);

        if (thisPlayerHasPuck)
        {
            // Player with puck moves towards target location
            if (targetLocations[currentTargetIndex] == null) return;
            targetPosition = targetLocations[currentTargetIndex].position;
        }
        else if (otherPlayerHasPuck)
        {
            // Player without puck moves to net location when the other player has the puck
            targetPosition = netLocation.position;
        }
        else
        {
            // Neither player has the puck, so chase the puck
            if (puckToFollow == null) return;
            targetPosition = puckToFollow.transform.position;
        }

        Vector3 directionToTarget = (targetPosition - playerRigidbody.position).normalized;
        Vector3 movement = new Vector3(directionToTarget.x, 0, directionToTarget.z);

        // Check if the player has reached the target location
        float distanceToTarget = Vector3.Distance(playerRigidbody.position, targetPosition);
        float stoppingDistance = 4f; 

        if (distanceToTarget <= stoppingDistance)
        {
            // Player has reached the target
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;

            // If the player has the puck and has reached the target rotate towards shoot location and then shoot
            if (thisPlayerHasPuck && shootLocation != null && !isShootingInProgress)
            {
                isShootingInProgress = true;
                StartCoroutine(RotateAndShoot(playerRigidbody));
            }
        }
        else
        {
            // Apply force for movement
            playerRigidbody.AddForce(movement * moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);

            // Limit velocity to maxSpeed
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(playerRigidbody.velocity, Vector3.up);
            if (horizontalVelocity.magnitude > maxSpeed)
            {
                Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
                playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
            }

            // Rotate the player towards the movement direction
            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private IEnumerator RotateAndShoot(Rigidbody playerRigidbody)
    {
        Vector3 directionToShootLocation = (shootLocation.transform.position - playerRigidbody.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToShootLocation);

        // Rotate towards shoot location
        while (Quaternion.Angle(playerRigidbody.rotation, targetRotation) > 1f)
        {
            playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Shooting
        if (playerRigidbody == leftRB)
        {
            ShootPuck(leftPlayer, leftPuckPos);
        }
        else if (playerRigidbody == rightRB)
        {
            ShootPuck(rightPlayer, rightPuckPos);
        }

        ChooseNextTarget(); 
        isShootingInProgress = false;
    }

    public void CollisionDetected(PlayerCollisions playerCollision)
    {
        //Debug.Log("Collided");
        if (Vector3.Distance(leftPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            leftRB.velocity = leftRB.velocity / 10;
            leftPlayerHasPuck = true;
            //Debug.Log("left player has puck");
            rightPlayerHasPuck = false;
        }
        else if (Vector3.Distance(rightPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            //reduce velocity
            rightRB.velocity = rightRB.velocity / 10;
            rightPlayerHasPuck = true;
            //Debug.Log("right player has puck");
            leftPlayerHasPuck = false;
        }
    }

    private void ShootPuck(GameObject player, Transform puckPosition)
    {
        // Instantiate the puck at the designated position
        var instance = Instantiate(puckPrefab, puckPosition.position, Quaternion.identity);

        // Get the forward direction of the player
        Vector3 shootDirection = player.transform.forward;

        // Apply force to the puck in the direction the player is facing
        instance.GetComponent<Rigidbody>().AddForce(shootDirection * 80, ForceMode.Impulse);

        // Remove puck from player
        if (player == leftPlayer)
        {
            leftPlayerHasPuck = false;
            leftPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            //Debug.Log("Left player shot the puck");
        }
        else if (player == rightPlayer)
        {
            rightPlayerHasPuck = false;
            rightPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            //Debug.Log("Right player shot the puck");
        }
    }

    public void TacklePlayer(Rigidbody playerRigidbody, GameObject player)
    {
        PlayerCollisions playerCollisions = player.GetComponent<PlayerCollisions>();
        if (!playerCollisions.hasPuck)
        {
            playerCollisions.isTackling = true;
            Vector3 lungeDirection = playerRigidbody.transform.forward;
            playerRigidbody.AddForce(lungeDirection * lungeForce, ForceMode.Impulse);

            //raycast to detect the tackled player
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, lungeDirection, out hit, 2f))
            {
                PlayerCollisions tackledPlayer = hit.collider.GetComponent<PlayerCollisions>();
                if (tackledPlayer != null && tackledPlayer.gameObject != player)
                {
                    tackledPlayer.GetTackled(lungeDirection * lungeForce);
                }
            }
        }
        ResetTackle(playerCollisions);
    }

    private IEnumerator ResetTackle(PlayerCollisions playerCollisions)
    {
        playerCollisions.isTackling = true;
        yield return new WaitForSeconds(tackleResetTime);
        playerCollisions.isTackling = false;
        playerCollisions.tackleCollider.enabled = false;
    }

    private IEnumerator HandlePass(GameObject passer, GameObject receiver, Transform puckPosition)
    {
        Vector3 directionToReceiver = (receiver.transform.position - passer.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToReceiver);
        float rotationSpeed = 40f;

        while (Quaternion.Angle(passer.transform.rotation, targetRotation) > 0.1f)
        {
            passer.transform.rotation = Quaternion.Slerp(passer.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        if (passer == leftPlayer)
        {
            leftPlayerHasPuck = false;
            //Debug.Log("Left player passed");
            leftPlayer.GetComponent<PlayerCollisions>().RemovePuck();
        }
        else if (passer == rightPlayer)
        {
            rightPlayerHasPuck = false;
            //Debug.Log("Right player passed");
            rightPlayer.GetComponent<PlayerCollisions>().RemovePuck();
        }

        var instance = Instantiate(puckPrefab, puckPosition.position, Quaternion.identity);
        instance.GetComponent<Rigidbody>().AddForce(directionToReceiver * 80, ForceMode.Impulse);
        //passCoroutine = null;
    }
}
