using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanalDataUpdate : MonoBehaviour
{
    InputActionAsset inputActions;
    //string[] playerMap;

    int useCount;
    int[] useIndex;
    string[] usePrefab;
    InputDevice[] useDevice;
    GameObject[] characterInputPrefabs01;
    GameObject[] characterInputPrefabs02;
    GameObject[] characterInputPrefabs03;
    GameObject[] characterInputPrefabs04;
    public GameObject[] inputCharacters;
    public PlayerInput[] playerInputs;
    string[] folderName;



    // Start is called before the first frame update
    void Start()
    {
        LoadAndSetPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void LoadAndSetPlayers()
    {
        //Setting Array Lengths
        useIndex = new int[4];
        usePrefab = new string[4];
        useDevice = new InputDevice[4];
        inputCharacters = new GameObject[4];
        playerInputs = new PlayerInput[4];
        folderName = new string[4] { "Inputs01", "Inputs02", "Inputs03", "Inputs04" };


        //Loading InputPlayers
        characterInputPrefabs01 = Resources.LoadAll<GameObject>(folderName[0]);
        characterInputPrefabs02 = Resources.LoadAll<GameObject>(folderName[1]);
        characterInputPrefabs03 = Resources.LoadAll<GameObject>(folderName[2]);
        characterInputPrefabs04 = Resources.LoadAll<GameObject>(folderName[3]);

        //Getting Data of Players
        Debug.Log(PersistantData.GetInstance().GetCount());
        useCount = PersistantData.GetInstance().GetCount();

        for (int i = 0; i < useCount; i++)
        {
            Debug.Log(PersistantData.GetInstance().GetIndex(i));
            useIndex[i] = PersistantData.GetInstance().GetIndex(i);
            Debug.Log(PersistantData.GetInstance().GetPrefab(i));
            usePrefab[i] = PersistantData.GetInstance().GetPrefab(i);
            Debug.Log(PersistantData.GetInstance().GetDevice(i));
            useDevice[i] = PersistantData.GetInstance().GetDevice(i);

            //Finding what character players selected and grabbing input equivalent
            if (i == 0)
            {
                for (int r = 0; r < characterInputPrefabs01.Length; r++)
                {
                    if ((usePrefab[i] + "Input01") == characterInputPrefabs01[r].name)
                    {
                        inputCharacters[i] = characterInputPrefabs01[r];
                    }
                }
            }
            else if (i == 1)
            {
                for (int r = 0; r < characterInputPrefabs02.Length; r++)
                {
                    if ((usePrefab[i] + "Input02") == characterInputPrefabs02[r].name)
                    {
                        inputCharacters[i] = characterInputPrefabs02[r];
                    }
                }
            }
            else if (i == 2)
            {
                for (int r = 0; r < characterInputPrefabs03.Length; r++)
                {
                    if ((usePrefab[i] + "Input03") == characterInputPrefabs03[r].name)
                    {
                        inputCharacters[i] = characterInputPrefabs03[r];
                    }
                }
            }
            else if (i == 3)
            {
                for (int r = 0; r < characterInputPrefabs04.Length; r++)
                {
                    if ((usePrefab[i] + "Input04") == characterInputPrefabs04[r].name)
                    {
                        inputCharacters[i] = characterInputPrefabs04[r];
                    }
                }
            }


            //Instantiate Players & Insert InputMap
            playerInputs[i] = PlayerInput.Instantiate(inputCharacters[i], playerIndex: useIndex[i], pairWithDevices: useDevice[i]);
            playerInputs[i].gameObject.transform.position = new Vector3(0f, 2f, 0f);
            Debug.Log(playerInputs[i].gameObject.name);

            Debug.Log(playerInputs[i].gameObject.GetComponent<PlayerInput>().actions);
            inputActions = playerInputs[i].gameObject.GetComponent<PlayerInput>().actions;


        }
    }
}
