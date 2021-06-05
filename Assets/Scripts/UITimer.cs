using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITimer : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft;
    public bool countingDown;

    // Start is called before the first frame update
    void Start()
    {
        countingDown = true;
        secondsLeft = 10;
        textDisplay.GetComponent<TextMeshProUGUI>().text = "";     
    }

    // Update is called once per frame
    void Update()
    {
        if (countingDown == false && secondsLeft > 0)
            StartCoroutine(TimerTake());
        else if (secondsLeft == 0)
            TransitionToBoardScene();
            
    }

    public void StartTimer()
    {
        Debug.Log("Time Start");
        textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
        countingDown = false;
    }

    public void TransitionToBoardScene()
    {
        //save select character data
        //PlayerPrefs.SetInt("characterModel#", characterNum);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("BoardSelectionScene");
    }


    IEnumerator TimerTake()
    {
        countingDown = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:0" + secondsLeft;
        else
            textDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
            
        countingDown = false;

    }
}
