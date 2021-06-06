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


    int playerMarkerIndex;
    PlayerInputManager playerInputManager;
    [SerializeField] public List<GameObject> playerMarker = new List<GameObject>();
   
    void Start()
    {
        characterSelectionHandler = GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionHandler>();

        playerInputManager = GetComponent<PlayerInputManager>();
        playerMarkerIndex = 0;
        Debug.Log("Marker Num" + playerMarker);
        playerInputManager.playerPrefab = playerMarker[playerMarkerIndex];
     

    }

    void JoinBehavior(PlayerInput input)
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        
        characterSelectionHandler.ButtonSelectOnUserLoad(playerMarkerIndex);
    
       
        playerMarkerIndex = playerMarkerIndex + 1;
        Debug.Log("Marker Num" + playerMarker);
        playerInputManager.playerPrefab = playerMarker[playerMarkerIndex];
  

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

}
