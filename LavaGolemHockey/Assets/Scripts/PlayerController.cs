using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Puck Variables
    //public Puck puckClass;
    //public GameObject puckVisualPrefab;
    //public GameObject puckPrefab;
    //public GameObject test;

    //Input Variables
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction moveLeft;
    private InputAction moveRight;

    //Player Variables
    public GameObject leftPlayer;
    public GameObject rightPlayer;
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
        leftPlayer.transform.Translate(new Vector3(movementInputLeft.x, 0, movementInputLeft.y) * speed * Time.deltaTime);
        rightPlayer.transform.Translate(new Vector3(movementInputRight.x, 0, movementInputRight.y) * speed * Time.deltaTime);
    }


    public void CollisionDetected(PlayerCollisions playerCollision)
    {
        Debug.Log("Collided");

        //reasarch collisions for "other" and for the actual logic you can do it on the player and make it send a message to this one for the status. 
        if (Vector3.Distance(leftPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            leftPlayerHasPuck = true;
            rightPlayerHasPuck = false;
            Destroy(playerCollision.gameObject);
        }
        else if (Vector3.Distance(rightPlayer.transform.position, playerCollision.transform.position) < 2f)
        {
            rightPlayerHasPuck = true;
            leftPlayerHasPuck = false;
            Destroy(playerCollision.gameObject);

        }
    }

    //private void OnCollisionEnter(Collision collision)
   // {
        
    //}

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInputLeft = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInputRight = ctx.ReadValue<Vector2>();

    public void OnLeftPass(InputAction.CallbackContext ctx)
    {
        //when the player has the puck
            //instantiate puck in front of player
            //set a force for the puck s
            //shoot the puck in the direction of teammate
            //gaurantee that the puck will reach the teammate unless intercepted
    }

    public void OnShootTackle(InputAction.CallbackContext ctx)
    {
        //When the player has the puck
            //get the direction of the player that has the puck
            //add force to the puck
            //
    }
}
