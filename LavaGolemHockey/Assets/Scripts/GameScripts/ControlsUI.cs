using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public static bool playersConnected = false;
    public static bool player1Joined = false;
    public static bool player2Joined = false;
    public bool pauseToggled = false;
    public GameObject controlsUI;
    public GameObject player1Controller;
    public GameObject player2Controller;
    public GameObject player1Conected;
    public GameObject player2Conected;


    private void Start()
    {
        controlsUI.SetActive(true);
    }
    public void ShowControls()
    {
        pauseToggled = true;
        controlsUI.SetActive(true);
    }

    private void Update()
    {
        if (player1Joined)
        {
            player1Controller.SetActive(false);
            player1Conected.SetActive(true);
            
        }

        if (player2Joined)
        {
            player2Controller.SetActive(false);
            player2Conected.SetActive(true);
        }

        if (GameStateManager.Instance.CurrentState == GameStateManager.GameState.Ready && !pauseToggled)
        {
            controlsUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && playersConnected)
        {
            controlsUI.SetActive(false);
            pauseToggled= false;
        }
    }
}
