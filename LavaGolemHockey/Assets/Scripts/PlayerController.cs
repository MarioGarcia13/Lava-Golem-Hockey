using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
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

    public GameObject puckPrefab;
    [SerializeField]
    private bool leftPlayerHasPuck = false;
    [SerializeField]
    private bool rightPlayerHasPuck = false;

    [Range(1f, 200f)]
    public float maxSpeed = 10f;
    [Range(1f, 8000f)]
    public float moveForce = 1000f;
    [Range(1f, 100f)]
    public float rotationSpeed = 10f;

    private Vector2 movementInputLeft;
    private Vector2 movementInputRight;

    private Coroutine passCoroutine;
    private bool canControl = false;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");
        leftRB = leftPlayer.GetComponent<Rigidbody>();
        rightRB = rightPlayer.GetComponent<Rigidbody>();

        GameStateManager.Instance.OnGameStateChanged += HandleGameStateChanged;

    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    public void HandleGameStateChanged(GameStateManager.GameState newState)
    {
        canControl = (newState == GameStateManager.GameState.Ready);
    }

    private void OnEnable()
    {
        moveLeft = player.FindAction("LSMove");
        moveRight = player.FindAction("RSMove");
        player.Enable();

    }
    private void OnDisable()
    {
        player.Disable();
    }
    private void FixedUpdate()
    {
        if (canControl)
        {
            MovePlayer(leftRB, movementInputLeft);
            MovePlayer(rightRB, movementInputRight);
        }
    }

    private void MovePlayer(Rigidbody playerRigidbody, Vector2 movementInput)
    {
        // Convert input to 3D vector
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);

        // Apply force for movement
        playerRigidbody.AddForce(movement * moveForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        // Limit velocity to maxSpeed
        Vector3 horizontalVelocity = Vector3.ProjectOnPlane(playerRigidbody.velocity, Vector3.up);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
        }

        // Rotate the player
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void CollisionDetected(PlayerCollisions playerCollision)
    {
        Debug.Log("Collided");
        if (Vector3.Distance(leftPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            leftRB.velocity = leftRB.velocity / 2;
            leftPlayerHasPuck = true;
            rightPlayerHasPuck = false;
        }
        else if (Vector3.Distance(rightPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
           //reduce velocity
            rightPlayerHasPuck = true;
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
        instance.GetComponent<Rigidbody>().AddForce(shootDirection * 50, ForceMode.Impulse); // Adjust force value as needed

        // Remove puck from player
        if (player == leftPlayer)
        {
            leftPlayerHasPuck = false;
            leftPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            Debug.Log("Left player shot the puck");
        }
        else if (player == rightPlayer)
        {
            rightPlayerHasPuck = false;
            rightPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            Debug.Log("Right player shot the puck");
        }
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
            Debug.Log("Left player passed");
            leftPlayer.GetComponent<PlayerCollisions>().RemovePuck();
        }
        else if (passer == rightPlayer)
        {
            rightPlayerHasPuck = false;
            Debug.Log("Right player passed");
            rightPlayer.GetComponent<PlayerCollisions>().RemovePuck();
        }

        var instance = Instantiate(puckPrefab, puckPosition.position, Quaternion.identity);
        instance.GetComponent<Rigidbody>().AddForce(directionToReceiver * 45, ForceMode.Impulse);
        passCoroutine = null;
    }


    public void OnLSMove(InputAction.CallbackContext ctx)
    {
        if (canControl)
        {
            movementInputLeft = ctx.ReadValue<Vector2>();
        }
    }

    public void OnRSMove(InputAction.CallbackContext ctx)
    {
        if (canControl)
        {
            movementInputRight = ctx.ReadValue<Vector2>();
        }
    }

    public void OnLeftPass(InputAction.CallbackContext ctx)
    {
        if (canControl && leftPlayerHasPuck && passCoroutine == null)
        {
            passCoroutine = StartCoroutine(HandlePass(leftPlayer, rightPlayer, leftPuckPos));
        }
    }

    public void OnRightPass(InputAction.CallbackContext ctx)
    {
        if (canControl && rightPlayerHasPuck && passCoroutine == null)
        {
            passCoroutine = StartCoroutine(HandlePass(rightPlayer, leftPlayer, rightPuckPos));
        }
    }

    public void OnLeftShootTackle(InputAction.CallbackContext ctx)
    {
        if (canControl && leftPlayerHasPuck)
        {
            ShootPuck(leftPlayer, leftPuckPos);
        }

    }
    
    public void OnRightShootTackle(InputAction.CallbackContext ctx)
    {
        if (canControl && rightPlayerHasPuck)
        {
            ShootPuck(rightPlayer, rightPuckPos);
        }
    }
}
