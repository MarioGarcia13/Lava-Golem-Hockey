using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Range(1f, 200f)]
    public float speed = 5f;

    private Vector2 movementInput;

    private void Update()
    {
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * speed * Time.deltaTime);
    }

    public void OnLSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnRSMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

}
