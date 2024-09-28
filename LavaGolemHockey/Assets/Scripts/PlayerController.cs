using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Puck Variables
    //public GameObject puckPrefab;

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

    private void Awake()
    {
        //puckClass = GetComponent<Puck>();


        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");
        leftRB = leftPlayer.GetComponent<Rigidbody>();
        rightRB = rightPlayer.GetComponent<Rigidbody>();

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
        MovePlayer(leftRB, movementInputLeft);
        MovePlayer(rightRB, movementInputRight);

        //leftPlayer.transform.Translate(new Vector3(movementInputLeft.x, 0, movementInputLeft.y) * speed * Time.deltaTime);
       // rightPlayer.transform.Translate(new Vector3(movementInputRight.x, 0, movementInputRight.y) * speed * Time.deltaTime);

        //leftRB.MovePosition(new Vector3(movementInputLeft.x, 0, movementInputLeft.y) * speed * Time.deltaTime);
        //rightRB.MovePosition(new Vector3(movementInputRight.x, 0, movementInputRight.y) * speed * Time.deltaTime);

        //if (movementInputLeft != Vector2.zero)
       // {
         //   Quaternion leftTargetRotation = Quaternion.LookRotation(new Vector3(movementInputLeft.x, 0, movementInputLeft.y));
         //   leftPlayer.transform.rotation = Quaternion.Slerp(leftPlayer.transform.rotation, leftTargetRotation, Time.deltaTime * 10f);
       // }
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

        //reasarch collisions for "other" and for the actual logic you can do it on the player and make it send a message to this one for the status. 

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

    //private void OnCollisionEnter(Collision collision)
   // {
        
    //}

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInputLeft = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInputRight = ctx.ReadValue<Vector2>();

    public void OnLeftPass(InputAction.CallbackContext ctx)
    {
        if (leftPlayerHasPuck)
        {
            leftPlayerHasPuck = false;
            Debug.Log("passed");
            leftPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            var instance = Instantiate(puckPrefab, leftPuckPos.position, Quaternion.identity);

            Vector3 directionRightPlayer = (rightPlayer.transform.position - leftPlayer.transform.position).normalized;

            instance.GetComponent<Rigidbody>().AddForce(directionRightPlayer * 45, ForceMode.Impulse);
        }


        //when the player has the puck
            //instantiate puck in front of player
            //set a force for the puck s
            //shoot the puck in the direction of teammate
            //gaurantee that the puck will reach the teammate unless intercepted
    }

    public void OnRightPass(InputAction.CallbackContext ctx)
    {
        if (rightPlayerHasPuck)
        {
            rightPlayerHasPuck = false;
            Debug.Log("passed");
            rightPlayer.GetComponent<PlayerCollisions>().RemovePuck();
            var instance = Instantiate(puckPrefab, rightPuckPos.position, Quaternion.identity);
            instance.GetComponent<Rigidbody>().AddForce(new Vector3(45f, 0f, 0f), ForceMode.Impulse);

        }
    }

    public void OnLeftShootTackle(InputAction.CallbackContext ctx)
    {
        //When the player has the puck
            //get the direction of the player that has the puck
            //add force to the puck
            //
    }
    
    public void OnRightShootTackle(InputAction.CallbackContext ctx)
    {
        //When the player has the puck
            //get the direction of the player that has the puck
            //add force to the puck
            //
    }
}
