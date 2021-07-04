using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PersistantData : MonoBehaviour
{

    protected int playerCount;
    protected int[] playerIndex;
    protected string[] playerPrefab;
    protected InputDevice[] playerDevices;

    static PersistantData instance;

    public static PersistantData GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerIndex = new int[4];
        playerPrefab = new string[4];
        playerDevices = new InputDevice[4];

        //Ensuring Class remains singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

  

    public void SetCount(int count)
    {
        playerCount = count;
    }

    public int GetCount()
    {
        return playerCount;
    }

    public void SetIndex(int i,int index)
    {
        playerIndex[i] = index;
    }

    public int GetIndex(int i)
    {
        return playerIndex[i];
    }

    public void SetPrefab(int i, string name)
    {
        playerPrefab[i] = name;
    }

    public string GetPrefab(int i)
    {
        return playerPrefab[i];
    }

    public void SetDevices(int i, InputDevice device)
    {
        playerDevices[i] = device;
    }

    public InputDevice GetDevice(int i)
    {
        return playerDevices[i];
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
