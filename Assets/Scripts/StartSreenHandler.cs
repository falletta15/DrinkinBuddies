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
    //Gamepad player1;
    //Gamepad player2;
    //Gamepad player3;
    //Gamepad player4;

    private void Awake()
    {
        EventSystem.current.GetComponent<EventSystem>();

        numDevices = InputSystem.devices.Count;
        numGamepads = Gamepad.all.Count;
        //Debug.Log("Device count is " + numDevices);
        //Debug.Log("Gamepad count is " + numGamepads);

        /*
        //Register Player Order @ start
        if (numGamepads > 0)
            if (numGamepads <= 4)
            {
                for (int i = 0; i < numGamepads; i++)
                {
                    InputSystem.SetDeviceUsage(Gamepad.all[i], "Player" + (i));
                }
            }
            else if (numGamepads >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    InputSystem.SetDeviceUsage(Gamepad.all[i], "Player" + (i));
                }
            }
            */
    }





    ////Show List of Register Devices (Order in Gamepad.all & Label) *Need to see if order is kept between disconnect * should be kept with InputDevice.deviceId
    //Debug.Log if Device is being Press and if seen as same device we know it's working

    ////Player Register Visual Update

    private void Start()
    {

    }


    private void Update()
    {
        //ESC exit
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;



        numDevices = InputSystem.devices.Count;
        numGamepads = Gamepad.all.Count;
        Debug.Log("Updated Device count is " + numDevices);
        Debug.Log("Updated Gamepad count is " + numGamepads);

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
