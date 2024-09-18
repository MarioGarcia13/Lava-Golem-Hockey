using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Puck puckClass;
    public GameObject puckVisualPrefab;
    public GameObject puckPrefab;
    public GameObject test;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    [SerializeField]
    static public bool hasPuck = false;

    [Range(1f, 200f)]
    public float speed = 5f;

    private Vector2 movementInput;

    private void Awake()
    {
        puckClass = GetComponent<Puck>();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("LeftControls");

        if (test == null)
        {
            return;
        }

        if (this == null)
        {
            return;
        }
    }
    private void OnEnable()
    {
        move = player.FindAction("LSMove");
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }
    private void Update()
    {
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Puck")
        {
            hasPuck = true;
            Destroy(collision.gameObject);
            puckVisualPrefab.SetActive(true);
        }
    }

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnLeftPass(InputAction.CallbackContext ctx)
    {

        if (hasPuck)
        {
            
            puckVisualPrefab.SetActive(false);
            test = Instantiate(puckPrefab, new Vector3 (0,0,0), transform.rotation);
            puckClass.shootPuck();

            Debug.Log("Pass Puck");
            hasPuck = false;
        }

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
