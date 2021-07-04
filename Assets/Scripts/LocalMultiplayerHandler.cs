using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
using System;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class LocalMultiplayerHandler : MonoBehaviour
{
    CharacterSelectionHandler characterSelectionHandler;


    int playerMarkerIndex;
    PlayerInputManager playerInputManager;
    [SerializeField] public List<GameObject> playerMarker = new List<GameObject>();

    
    /// /WIP
    int sendCount;
    int[] sendIndex;
    string[] sendPrefab;
    PlayerInput[] userPrefabInput;
    InputDevice[] sendDevices;

    bool runInputDisconnect = false;

    void Start()
    {
        Debug.Log("Local Multiplayer Handler exists in script");

        characterSelectionHandler = GameObject.Find("CharacterSelectionManager").GetComponent<CharacterSelectionHandler>();

        playerInputManager = GetComponent<PlayerInputManager>();
        playerMarkerIndex = 0;
        Debug.Log("Marker Num" + playerMarker);
        playerInputManager.playerPrefab = playerMarker[playerMarkerIndex];


        sendIndex = new int[4];
        sendPrefab = new string[4];
        userPrefabInput = new PlayerInput[4];
        sendDevices = new InputDevice[4];
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("The number of users: " + InputUser.all.Count);

        if (runInputDisconnect == true)
        {
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

    }

    public void SetRunInputDisconnectTrue()
    {
        runInputDisconnect = true;
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
                //GameObject.Find("PlayerMarker" + (i + 1) + "(Clone)").GetComponent<SpriteRenderer>().enabled = false;
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
                
                //GameObject.Find("PlayerMarker" + (i + 1) + "(Clone)").GetComponent<SpriteRenderer>().enabled = true;
                if (characterSelectionHandler.characterLoaded[i] == true)
                    characterSelectionHandler.ShowCharacter(i);
            }
                
            
        }
    }

    public void CapturePlayerData()
    {
        //var m_Players = PlayerInput.all(x => new MyPlayerData { devices = x.devices.ToArray(), index = x.playerIndex }).ToArray();

        //Debug.Log("Transfer Count:" + InputUser.all.Count); //Count

        for (int i=0; i<InputUser.all.Count; i++)
        {
            //Debug.Log(i + ":Transfer Index = " + i);    //Index
            //Debug.Log(i + ":Transfer Player Input = " + PlayerInput.GetPlayerByIndex(i).name);  //Show matching of index
            //Debug.Log(i + "Transfer Prefab = " + characterSelectionHandler.currentCharacter[i].name);   //Prefab

            //Need device
            userPrefabInput[i] = GameObject.Find("PlayerMarker" + (i+1) + "(Clone)").GetComponent<PlayerInput>();

            //Debug.Log("PIV:" + userPrefabInput[i].devices[0]);
            //Debug.Log("PI:" + userPrefabInput[i].devices[0].name);


            //Set Persistant Variable for Player
            sendCount = InputUser.all.Count;
            sendIndex[i] = i;
            sendPrefab[i] = characterSelectionHandler.currentCharacter[i].name;
            sendDevices[i] = userPrefabInput[i].devices[0];

            //Send Persistant Variable for Player
            PersistantData.GetInstance().SetCount(sendCount);
            PersistantData.GetInstance().SetIndex(i, i);
            PersistantData.GetInstance().SetPrefab(i, sendPrefab[i]);
            PersistantData.GetInstance().SetDevices(i, sendDevices[i]);


            // Debug.Log(i + ":Transfer Device = " + InputUser.all[i].pairedDevices.ToArray());
            //Should work below
            //sendDevices = PlayerInput.GetPlayerByIndex(i).devices.ToArray();
            //Debug.Log(sendDevices);

            //Debug.Log("Hash Code:" + InputUser.all[i].pairedDevices.GetHashCode());
            //Debug.Log("Enumerator Code:" + InputUser.all[i].pairedDevices.GetEnumerator());
            //Debug.Log("Type Code:" + InputUser.all[i].pairedDevices.GetType());

            ////



        }

    }
    
}
