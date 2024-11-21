using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool toggle = false;
    public GameObject keyboardInstructions;
    public GameObject controllerInstructions;


    public void toggleInstructions()
    {
        toggle = !toggle;
        if (toggle)
        {
            controllerInstructions.SetActive(false);
            keyboardInstructions.SetActive(true);
        }
        else
        {
            keyboardInstructions.SetActive(false);
            controllerInstructions.SetActive(true);
        }
    }


}
