using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class StartSreenHandler : MonoBehaviour
{

    [SerializeField] GameObject MainMenuGO;
    [SerializeField] GameObject OptionMenuGO;

    public GameObject optionsMenuFirstButton, mainMenuFirstButton;


    int numDevices;
    int numGamepads;

    bool runInputDisconnect = false;

    private void Awake()
    {
        EventSystem.current.GetComponent<EventSystem>();

        numDevices = InputSystem.devices.Count;
        numGamepads = Gamepad.all.Count;
       
    }

    private void Start()
    {

    }


    private void Update()
    {

        numDevices = InputSystem.devices.Count;
        numGamepads = Gamepad.all.Count;
        //Debug.Log("Updated Device count is " + numDevices);
        //Debug.Log("Updated Gamepad count is " + numGamepads);

        if (numGamepads >= 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;   
        }

        if (numGamepads == 0)
        { 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        
        if (runInputDisconnect == true)
        {
            InputSystem.onDeviceChange +=
            (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        Debug.Log("New device added: " + device);
                        CheckIfMenuActive();
                        break;

                    case InputDeviceChange.Removed:
                        Debug.Log("Device removed: " + device);
                        break;
                }
            };
        }
        
    }

    public void SetRunInputDisconnectTrue()
    {
        runInputDisconnect = true;
    }

    public void TransitiontoCharacterSelectScene()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    private void CheckIfMenuActive()
    {
        if (MainMenuGO.activeSelf == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
        }
    }

    public void ActivateMainMenu()
    {
        
        HideOptionMenu();
        ShowMainMenu();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void ActivateOptionMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        HideMainMenu();
        ShowOptionMenu();

        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    public void ActivateExitGame()
    {
        Exit();
    }

    public void ShowMainMenu()
    {
        MainMenuGO.SetActive(true);
    }

    public void ShowOptionMenu()
    {
        OptionMenuGO.SetActive(true);
    }

    public void HideMainMenu()
    {
        MainMenuGO.SetActive(false);
    }

    public void HideOptionMenu()
    {
        OptionMenuGO.SetActive(false);
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
