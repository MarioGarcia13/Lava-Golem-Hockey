using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject puckPrefab;

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
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("LeftControls");

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
            puckPrefab.SetActive(true);
        }
    }

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnPass(InputAction.CallbackContext ctx)
    {

        if (hasPuck)
        {
            //Debug.Log("Pass Puck");
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
