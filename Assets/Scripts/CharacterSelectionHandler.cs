using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem.UI;
using System;
using UnityEngine.UI;

public class CharacterSelectionHandler : MonoBehaviour
{
    UITimer uiTimer;
    LocalMultiplayerHandler localMultiplayerHandler;

    //Need 4 Player characterNum and characterIndex
    public int[] characterNum;
    public int[] characterIndex;
    int readyCounter;

    GameObject[] characterPrefabs;
    public GameObject[] currentCharacter;
    public bool[] characterLoaded;

    [SerializeField] TextMeshProUGUI[] characterNameDisplayTM;
    [SerializeField] GameObject[] MESbutton;

    int buttonIndexL;
    int buttonIndexR;
    [SerializeField] Button btnL01;
    [SerializeField] Button btnL02;
    [SerializeField] Button btnL03;
    [SerializeField] Button btnL04;
    [SerializeField] Button btnR01;
    [SerializeField] Button btnR02;
    [SerializeField] Button btnR03;
    [SerializeField] Button btnR04;
    Button[] buttonLArray;
    Button[] buttonRArray;
    

    // Start is called before the first frame update
    void Start()
    {
        localMultiplayerHandler = GameObject.Find("PlayerManager").GetComponent<LocalMultiplayerHandler>();

        characterNum = new int[4];
        characterIndex = new int[4];

        for (int i=0; i<characterNum.Length; i++)
        {
            characterNum[i] = 0;
        }


        for (int i = 0; i < characterIndex.Length; i++)
        {
            characterIndex[i] = 5;
        }


        currentCharacter = new GameObject[4];
        characterLoaded = new bool[4];

        buttonLArray = new Button[4] { btnL01, btnL02, btnL03, btnL04 };
        buttonRArray = new Button[4] { btnR01, btnR02, btnR03, btnR04 };

        //Need to keep track of each player current player index
        buttonLArray[0].onClick.AddListener(() => PreviousCharacter(0));
        buttonLArray[1].onClick.AddListener(() => PreviousCharacter(1));
        buttonLArray[2].onClick.AddListener(() => PreviousCharacter(2));
        buttonLArray[3].onClick.AddListener(() => PreviousCharacter(3));
        buttonRArray[0].onClick.AddListener(() => NextCharacter(0));
        buttonRArray[1].onClick.AddListener(() => NextCharacter(1));
        buttonRArray[2].onClick.AddListener(() => NextCharacter(2));
        buttonRArray[3].onClick.AddListener(() => NextCharacter(3));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<characterLoaded.Length; i++)
        if (characterLoaded[i] == true)
        {
            RotateObject(currentCharacter[i], 20.0f); 
            UpdateCharacterName(currentCharacter[i], i);
        }

    }


    public void LoadCharacter(Vector3 playerMarkerPositon, int playerIndex)
    {

        //Set character index and load character into scene
        characterPrefabs = Resources.LoadAll<GameObject>("Characters");

        //Location for character models
        Vector3 characterPositionCoord = playerMarkerPositon;

        //Instantiate character model
        currentCharacter[playerIndex] = Instantiate(characterPrefabs[characterNum[playerIndex]],characterPositionCoord,Quaternion.identity);
        currentCharacter[playerIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        characterLoaded[playerIndex] = true;
    }

    public void HideCharacter(int playerIndex)
    {
        currentCharacter[playerIndex].GetComponent<MeshRenderer>().enabled = false;
    }

    public void ShowCharacter(int playerIndex)
    {
        currentCharacter[playerIndex].GetComponent<MeshRenderer>().enabled = true;
    }

    public void PreviousCharacter(int playerIndex) 
    {
        Debug.Log("Previous Character Click");
        Debug.Log("Button: " + EventSystem.current.currentSelectedGameObject.name);
        Debug.Log("Active Scripting" + characterIndex[playerIndex]);
        Debug.Log("PI: " + playerIndex);

        if (characterNum[playerIndex] != 0)
        {
            characterNum[playerIndex]--;
        }
        else if (characterNum[playerIndex] == 0)
        {
            characterNum[playerIndex] = characterIndex[playerIndex];
        }

        Destroy(currentCharacter[playerIndex]);
        currentCharacter[playerIndex] = Instantiate(characterPrefabs[characterNum[playerIndex]], currentCharacter[playerIndex].transform.position , currentCharacter[playerIndex].transform.rotation);
        currentCharacter[playerIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        UpdateCharacterName(currentCharacter[playerIndex], playerIndex);
        
    }

    public void NextCharacter(int playerIndex) 
    {
        Debug.Log("Previous Character Click");
        Debug.Log("Button: " + EventSystem.current.currentSelectedGameObject.name);
        Debug.Log("Active Scripting" + characterIndex[playerIndex]);
        Debug.Log("PI: " + playerIndex);

        if (characterNum[playerIndex] != characterIndex[playerIndex])
        {
            characterNum[playerIndex]++;
        }
        else if (characterNum[playerIndex] == characterIndex[playerIndex])
        {
            characterNum[playerIndex] = 0;
        }

        Destroy(currentCharacter[playerIndex]);
        currentCharacter[playerIndex] = Instantiate(characterPrefabs[characterNum[playerIndex]], currentCharacter[playerIndex].transform.position, currentCharacter[playerIndex].transform.rotation);
        currentCharacter[playerIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        UpdateCharacterName(currentCharacter[playerIndex], playerIndex);

    }

    public void RotateObject(GameObject characterObject, float rotationSpeed)
    {
        characterObject.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
    }

    public void UpdateCharacterName(GameObject characterObject, int playerIndex)
    {
        characterObject.name = characterObject.name.Replace("(Clone)", "");
       characterNameDisplayTM[playerIndex].SetText(characterObject.name);
    }


    public void UserReadyUp()
    {
        uiTimer = GameObject.Find("TimerManager").GetComponent<UITimer>();

        readyCounter++;
        Debug.Log("User is ready: " + readyCounter);


        if (readyCounter >= 4)
        {

            //Add code to send data on player manager devices & prefab link
            localMultiplayerHandler = GameObject.Find("PlayerManager").GetComponent<LocalMultiplayerHandler>();
            localMultiplayerHandler.CapturePlayerData();

            uiTimer.StartTimer();
        }
            

    }

    public void ButtonSelectOnUserLoad(int playerIndex)
    {
        GameObject.Find("MultiplayerEventSystem0" + (playerIndex +1)).GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(MESbutton[playerIndex]); 
    }
}
