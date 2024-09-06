using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*
    [Range(1f, 200f)]
    public float speed = 5f;

    private Vector2 movementInput;

    private void Update()
    {
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    }

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    */

    [SerializeField]
    [Range(0f, 200f)]
    private float MoveSpeed = 3f;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);   
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
