using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    [Range(1f, 200f)]
    public float speed = 5f;

    private Vector2 movementInput;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("LeftControls");
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
        if (this == null)
        {
            return;
        }

        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    }

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnPass(InputAction.CallbackContext ctx)
    {
        //when the player has the puck
            //set a force for the puck
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
