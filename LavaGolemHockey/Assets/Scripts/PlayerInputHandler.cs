using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerController playerController;

    private Controls controls;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        controls = new Controls();
    }

    public void InitializePlayer(PlayerConfiguration GamePad)
    {
        playerConfig = GamePad;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.LeftControls.LSMove.name)
        {
            OnMove(obj);
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.SetInputVector(context.ReadValue<Vector2>());
        }
    }
}
