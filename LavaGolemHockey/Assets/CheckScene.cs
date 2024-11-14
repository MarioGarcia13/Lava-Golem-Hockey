using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckScene : MonoBehaviour
{
    public int GetCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        return sceneIndex;
    }

}
