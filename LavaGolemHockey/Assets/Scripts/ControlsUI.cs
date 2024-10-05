using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public GameObject controlsUI;
    private void Start()
    {
        
    }
    public void ShowControls()
    {
        controlsUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controlsUI.SetActive(false);
        }
    }
}
