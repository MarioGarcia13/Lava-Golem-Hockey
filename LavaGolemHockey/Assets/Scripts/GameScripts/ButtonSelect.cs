using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public bool hasChanged = false;
    public Button singlePlayerButton;
    public Button primaryButton;
    // Start is called before the first frame update
    void Start()
    {
        primaryButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasChanged)
        {
            singlePlayerButton.Select();
            hasChanged = true;
        }
    }
}
