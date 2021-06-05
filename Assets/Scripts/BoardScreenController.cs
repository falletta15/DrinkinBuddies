using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardScreenController : MonoBehaviour
{

    public void TransitiontoBoard00()
    {
        SceneManager.LoadScene("Board00");
    }

}
