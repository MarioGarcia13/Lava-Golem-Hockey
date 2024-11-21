using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance;

    //images
    [SerializeField]
    GameObject LGHStart;
    [SerializeField]
    GameObject LGHBackground;
    [SerializeField]
    GameObject WinScreenP1;
    [SerializeField]
    GameObject WinScreenP2;

    //buttons
    [SerializeField]
    GameObject StartButton;
    [SerializeField]
    GameObject ModeSelect;

    [Range(0f, 30f)]
    public float WinScreenTime = 30f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShowMenu()
    {
        StartButton.SetActive(false);
        LGHStart.SetActive(false);
        LGHBackground.SetActive(true);
        ModeSelect.SetActive(true);
    }

    public void HideMenu()
    {
        LGHBackground.SetActive(false);
        ModeSelect.SetActive(false);
    }

    public void GameScene()
    {
        HideMenu();
        SceneManager.LoadScene(1);
    }

    public void SinglePlayer()
    {
        HideMenu();
        SceneManager.LoadScene(2);
    }

    public void ShowWinScreenP1()
    {
        StartCoroutine(P1Timer());
        ShowMenu();
    }

    public void ShowWinScreenP2()
    {
        StartCoroutine(P2Timer());
        ShowMenu();
    }

    IEnumerator P1Timer()
    {
        WinScreenP1.SetActive(true);
        yield return new WaitForSeconds(WinScreenTime);
        WinScreenP1.SetActive(false);
        SceneManager.LoadScene(0);
    }

    IEnumerator P2Timer()
    {
        WinScreenP2.SetActive(true);
        yield return new WaitForSeconds(WinScreenTime);
        WinScreenP2.SetActive(false);   
        SceneManager.LoadScene(0);
    }

    
}
