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
    static public bool hasPuck = false;
    [Range(1f, 200f)]
    public float speed = 10f;
    private Vector2 movementInputLeft;
    private Vector2 movementInputRight;

    private void Awake()
    {
        //puckClass = GetComponent<Puck>();


        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("PlayerControls");

        //leftPlayer = GameObject.FindGameObjectWithTag("left");
        //rightPlayer = GameObject.FindGameObjectWithTag("right");

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
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Puck")
        //{
            //hasPuck = true;
            //Destroy(collision.gameObject);
            //puckVisualPrefab.SetActive(true);
        //}
    }

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
