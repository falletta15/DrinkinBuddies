using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
using System;
using UnityEngine.InputSystem.UI;

public class LocalMultiplayerHandler : MonoBehaviour
{
    CharacterSelectionHandler characterSelectionHandler;

    //int deviceCompare;
    int playerMarkerIndex;
    PlayerInputManager playerInputManager;
    [SerializeField] public List<GameObject> playerMarker = new List<GameObject>();
    //int i = 0;
    //[SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        characterSelectionHandler = GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionHandler>();

        playerInputManager = GetComponent<PlayerInputManager>();
        playerMarkerIndex = 0;
        Debug.Log("Marker Num" + playerMarker);
        playerInputManager.playerPrefab = playerMarker[playerMarkerIndex];
        //Debug.Log("User paired device: " + InputUser.all[0].pairedDevices.Count);

    }

    void JoinBehavior(PlayerInput input)
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (InputUser.all[0].pairedDevices.Count == 0)
        //GameObject.Find("PlayerMarker0(Clone)").SetActive(false);
        //if (InputUser.all[0].pairedDevices.Count == 1)
        //GameObject.Find("PlayerMarker0(Clone)").SetActive(true);
        /*
        // Listening must be enabled explicitly.
        //++InputUser.listenForUnpairedDeviceActivity;

        // Example of how to spawn a new player automatically when a button
        // is pressed on an unpaired device.
        InputUser.onUnpairedDeviceUsed +=
            (control, eventPtr) =>
            {
        // Ignore anything but button presses.
        if (!(control is ButtonControl))
                    return;

                // Spawn player and pair device. If the player's actions have control schemes
                // defined in them, PlayerInput will look for a compatible scheme automatically.
                if (GameObject.Find(playerPrefab.name + "(Clone)") == null && control.device.deviceId != deviceCompare)
                    {
                        //PlayerInput.Instantiate(playerPrefab, pairWithDevice: control.device);
                        //deviceCompare = control.device.deviceId;
                    }           
                else
                    return;
            };
            */
        Debug.Log("The number of users: " + InputUser.all.Count);

            InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    PlayerMarkerRenderingFalse();
                    break;
                case InputDeviceChange.Reconnected:
                    // Plugged back in.
                    PlayerMarkerRenderingTrue();
                    break;
                case InputDeviceChange.Removed:
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
        
    }


    
    public void JoinedCreateMarker(PlayerInput input)
    {
        characterSelectionHandler.LoadCharacter(playerInputManager.playerPrefab.transform.position, playerMarkerIndex);

        //*Link to UIInputSystem and PlayerMarkers seem unessary
        //var eve = GameObject.Find("MultiplayerEventSystem01").GetComponent<InputSystemUIInputModule>();
        //var multeve = GameObject.Find("PlayerMarker" + (playerMarkerIndex + 1) + "(Clone)").GetComponent<PlayerInput>().uiInputModule;
        characterSelectionHandler.ButtonSelectOnUserLoad(playerMarkerIndex);
        //multeve = eve;

        //Debug.Log("Content of UI Input System: " + multeve);

        playerMarkerIndex = playerMarkerIndex + 1;
        Debug.Log("Marker Num" + playerMarker);
        playerInputManager.playerPrefab = playerMarker[playerMarkerIndex];
        
        //i = i + 1;
        //Debug.Log("Player Input: " + PlayerInput.all[0]);

    }

    public void PlayerMarkerRenderingFalse()
    {
     
        
        for (int i=0; i<InputUser.all.Count; i++)
        {
            Debug.Log("User paired device: " + i + ": " + InputUser.all[i].pairedDevices.Count);
            Debug.Log("Render False PlayerMarker" + (i + 1) + "(Clone)");

            if (InputUser.all[i].pairedDevices.Count == 0)
            {               
                GameObject.Find("PlayerMarker" + (i + 1) + "(Clone)").GetComponent<SpriteRenderer>().enabled = false;
                if (characterSelectionHandler.characterLoaded[i] == true)
                    characterSelectionHandler.HideCharacter(i);
            }
                
        }
    }

    public void PlayerMarkerRenderingTrue()
    {
        
        for (int i=0; i<InputUser.all.Count; i++)
        {
            Debug.Log("User paired device: " + i + ": "  + InputUser.all[i].pairedDevices.Count);
            Debug.Log("Render True PlayerMarker" + (i + 1) + "(Clone)");

            if (InputUser.all[i].pairedDevices.Count > 0)
            {
                
                GameObject.Find("PlayerMarker" + (i + 1) + "(Clone)").GetComponent<SpriteRenderer>().enabled = true;
                if (characterSelectionHandler.characterLoaded[i] == true)
                    characterSelectionHandler.ShowCharacter(i);
            }
                
            
        }
    }

    /*
    public void LeftRemoveMarker(PlayerInput input)
    {
        Object.Destroy(input.gameObject);
       
    }

    void OnDestroyPlayerMarkerGameObject()
    {
        //gameObject);
    }

    public void LeftRemoveMarkerContext(InputAction.CallbackContext context)
    {
        Debug.Log("PlayerDisconnected from Context");
        Object.Destroy(this.gameObject);
    }

    public void DisconnectRemoveMarker()
    {
        Destroy(this.gameObject);
    }
    */
}
