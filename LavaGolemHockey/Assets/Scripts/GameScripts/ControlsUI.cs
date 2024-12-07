using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering.Universal;

public class ControlsUI : MonoBehaviour
{
    public static bool playersConnected = false;
    public static bool player1Joined = false;
    public static bool player2Joined = false;
    public bool turnOffLoop = false;
    public bool togglePause = false;
    public bool startPressed = false;
    public GameObject controlsUI;
    public GameObject player1Controller;
    public GameObject player2Controller;
    public GameObject player1Conected;
    public GameObject player2Conected;
    public CheckScene checkScene;

    private void Start()
    {
        controlsUI.SetActive(true);

        if (player2Controller == null)
        {
            return;
        }
    }
    /*public void ShowControls()
    {
        pauseToggled = true;
        controlsUI.SetActive(true);
    }*/

    private void Update()
    {
        if (player1Joined)
        {
            player1Controller.SetActive(false);
            player1Conected.SetActive(true);
            
        }

        if (checkScene.GetCurrentScene() != 2)
        {
            if (player2Joined)
            {
                player2Controller.SetActive(false);
                player2Conected.SetActive(true);
            }
        }
        
        if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.Ready)
        {
            if (!turnOffLoop)
            {
                controlsUI.SetActive(false);
                turnOffLoop = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameStateManager.Instance.CurrentState == GameStateManager.GameState.Ready)
        {
            togglePause = !togglePause;
            if (togglePause)
            {
                controlsUI.SetActive(togglePause);
                Time.timeScale = 0f;
            }
            if (!togglePause)
            {
                Time.timeScale = 1f;
                controlsUI.SetActive(togglePause);
            }
        }
    }
}
