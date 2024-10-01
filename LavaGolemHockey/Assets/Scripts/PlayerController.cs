using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float speed = 10f;
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
    private void Update()
    {
        if (canControl)
        {
            MovePlayer(leftRB, movementInputLeft);
            MovePlayer(rightRB, movementInputRight);
        }
    }

    private void MovePlayer(Rigidbody playerRigidbody, Vector2 movementInput)
    {
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.y));
            playerRigidbody.MoveRotation(Quaternion.Slerp(playerRigidbody.rotation, targetRotation, Time.deltaTime * 10));
        }
    }

    public void CollisionDetected(PlayerCollisions playerCollision)
    {
        Debug.Log("Collided");
        if (Vector3.Distance(leftPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            leftPlayerHasPuck = true;
            rightPlayerHasPuck = false;
        }
        else if (Vector3.Distance(rightPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
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
