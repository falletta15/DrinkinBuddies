using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHandler : MonoBehaviour
{
    static GameManagerHandler instance;

    StartSreenHandler startSreenHandler;
    LocalMultiplayerHandler localMultiplayerHandler;

    // Start is called before the first frame update
    void Start()
    {
        //Ensuring Class remains singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        CheckScene();
    }

    // Update is called once per frame
    void Update()
    {
        //ESC exit
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEditor.EditorApplication.isPlaying = false;
    }

    void CheckScene()
    {
        if (SceneManager.GetActiveScene().name == "StartScreen")
            startSreenHandler.SetRunInputDisconnectTrue();
        else if (SceneManager.GetActiveScene().name == "CharacterSelect")
            localMultiplayerHandler.SetRunInputDisconnectTrue();

    }
}
