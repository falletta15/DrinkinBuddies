using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardScreenController : MonoBehaviour
{
    //Move to Game Manager
    public void TransitiontoBoard00()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

}
